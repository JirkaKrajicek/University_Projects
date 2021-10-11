/**
 * @file       heap.c
 * @author     Ondřej Ševčík
 * @date       6/2019
 * @brief      Implementing of functions for heap
 * **********************************************************************
 * @par       COPYRIGHT NOTICE (c) 2019 TBU in Zlin. All rights reserved.
 */

/* Private includes -------------------------------------------------------- */
#include "heap.h"

#include <stdlib.h>
#include <string.h>

bool Heap_Init(tHeap *heap) {

    if(heap==NULL) return false;
    heap->array = myMalloc(MAX_ITEMS_START * sizeof(Data_t));
    if (heap->array == NULL) return false;

    heap->count=0;
    heap->maxItems=MAX_ITEMS_START;

    return true;
}

bool Heap_Insert(tHeap *heap, Data_t insertData) {

    if (heap == NULL) return false;
    unsigned size = heap->count;

    if ((size + 1) > heap->maxItems) {
        Data_t *p = myRealloc(heap->array, (heap->maxItems * 2) * sizeof(Data_t));

        if (p == NULL) return false;
        heap->array = p;
        heap->maxItems *= 2;
    }

    heap->array[size] = insertData;

    while (size >= 1 && Data_Cmp(&heap->array[size / 2], &insertData) >= 0) {
        Heap_Swap(heap, size / 2, size);
        size /= 2;
    }

    ++heap->count;

    return true;
}

void Heap_Destruct(tHeap *heap) {
    if(heap==NULL) return;
    myFree((heap)->array);
    heap->array=NULL;
    heap->maxItems = heap->count = 0;
    myFree(heap);
    heap = NULL;
}

bool Heap_FindMin(tHeap heap, Data_t *value) {
    if(heap.count == 0 || value == NULL) return false;
    /* if(heap.count == 1)
    {
        *value = heap.array[0];
        return true;
    }

    for(unsigned int i =1; i < heap.count; ++i){
        int cmpare = Data_Cmp(&heap.array[i-1], &heap.array[i]);
        if(cmpare < 0) *value = heap.array[i-1];
        else *value = heap.array[i];
    }*/
    *value = heap.array[0];
    return true;

}

void heapify(tHeap *heap, int n, int i)
{
    if(heap==NULL) return;
    int largest = i; // Initialize largest as root
    int l = 2 * i+1;
    int r = 2 * i+2;

    //If left child is larger than root
    if (l < n && Data_Cmp(&heap->array[l], &heap->array[largest]) < 0)
        largest = l;
    // If right child is larger than largest so far
    if (r < n && Data_Cmp(&heap->array[r], &heap->array[largest]) < 0)
        largest = r;

    // If largest is not root
    if (largest != i) {
        Heap_Swap(heap,i,largest);
        heapify(heap, n, largest);
    }
}

bool Heap_DeleteMin(tHeap *heap, Data_t *deleteValue) {    
    if(heap==NULL || heap->count==0 || deleteValue == NULL) return false;
    *deleteValue = heap->array[0];
    int size = heap->count;
    Data_t lastElement = heap->array[size-1];

    // Replace root with first element
    heap->array[0] = lastElement;

    // Decrease size of heap by 1
    heap->count--;
    size = size - 1;

    // heapify the root node
    heapify(heap, size, 0);

    return true;
}

void Heap_Print(tHeap heap) {
    if(heap.count <= 0) return;
    for(unsigned int i = 0; i < heap.count; ++i)
    {
        printf("Index %d \t", i+1);
        Data_Print(&heap.array[i]);
    }

}

bool Heap_Empty(tHeap heap) {
    if(heap.count > 0) return false;
    else return true;
}

unsigned Heap_Count(tHeap heap) {
    return heap.count;
}

void Heap_Swap(tHeap *heap, unsigned index1, unsigned index2) {
    if(heap==NULL) return;
    Data_t tmp = heap->array[index1];
    heap->array[index1] = heap->array[index2];
    heap->array[index2] = tmp;

}
