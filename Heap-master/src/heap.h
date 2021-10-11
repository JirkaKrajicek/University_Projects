/**
 * @file       heap.h
 * @author     Ondřej Ševčík
 * @date       6/2019
 * @brief      Header file for heap
 * **********************************************************************
 * @par       COPYRIGHT NOTICE (c) 2019 TBU in Zlin. All rights reserved.
 */

/* Public includes -------------------------------------------------------- */
#include <stdbool.h>
#include <stdio.h>

#include "data.h"
#include "mymalloc.h"

#ifndef HEAP_H
#define HEAP_H

/* Public defines --------------------------------------------------------- */
#define MAX_ITEMS_START \
  4 /**< Maximum items in heap that can be stored when initializing heap */

/* Heap structure ---------------------------------------------------------- */
typedef struct {
  Data_t *array;     /**< Array, where are the items being stored */
  unsigned int count;    /**< Keeps track of how many items are in heap */
  unsigned int maxItems; /**< Count how many items can be stored in heap */
} tHeap;
/**
 * @brief Heap_Init  Initializes the array, sets heap count to 0 and allocates
 * array memory to MAX_ITEMS_START and sets maxItems to that value
 * @param heap[in] Heap which we want to initialize
 * @return  Returns true if heap was initialized, return false otherwise
 */
bool Heap_Init(tHeap *heap);

/**
 * @brief Heap_Insert    Inserts the value into the heap according to the rules
 * for heap. If the count of items in heap is higher than maxItems, reallocates
 * the memory of heap to double size.
 * @param[in] heap Heap where the data are being stored
 * @param[in] insertData    Data that are being stored in heap
 * @return  Returns true if the data were inserted, return false otherwise
 */
bool Heap_Insert(tHeap *heap, Data_t insertData);

/*!
 * @brief Destruct the heap(Free's the pointer array at nodes and sets the size
 * to NULL)
 * @param[in] table Pointer at heap we want to destruct
 */
void Heap_Destruct(tHeap *heap);

/**
 * @brief Heap_FindMin   Returns the minimum value in heap
 * @param[in] heap Heap where we want to find the minimum value
 * @param[out] value Pointer to a value to be returned.
 * @return  TRUE if the item is in the heap, else returns FALSE if
 * there are no data in heap
 */
bool Heap_FindMin(tHeap heap, Data_t *value);

/**
 * @brief Heap_DeleteMin Deletes the minimum value that is stored in heap
 * @param[in] heap Pointer at our heap
 * @param[out] deleteValue Pointer to the data that are being deleted. (optional, can be NULL)
 * @return  TRUE if the item is be deleted, else returns FALSE if
 * there are no data in heap
 */
bool Heap_DeleteMin(tHeap *heap,Data_t *deleteValue);

/**
 * @brief Heap_Empy  Check if heap is empty
 * @param heap[in] Heap which we want to check
 * @return Return true if the heap is empty, return false otherwise
 */
bool Heap_Empty(tHeap heap);

/**
 * @brief Heap_Size  Returns the size of heap
 * @param heap[in] Heap that we want size of
 * @return Returns the number of items stored in heap
 */
unsigned int Heap_Count(tHeap heap);

/**
 * @brief Heap_Print Prints the indexes and items from heap
 * @param heap[in] Heap that we want to print
 */
void Heap_Print(tHeap heap);

/**
 * @brief Heap_Swap Swaps 2 items in heap
 * @param heap[in]     Pointer at heap where we want to swap items
 * @param index1f[in]   Index of an item that we want to swap
 * @param index2[in]    Index of an item that we want to swap
 */
void Heap_Swap(tHeap *heap, unsigned index1, unsigned index2);

#endif  // HEAP_H
