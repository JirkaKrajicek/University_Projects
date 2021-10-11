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
  (void)heap;
  return false;
}

bool Heap_Insert(tHeap *heap, Data_t insertData) {
  (void)heap;
  (void)insertData;
  return true;
}

void Heap_Destruct(tHeap *heap) { (void)heap; }

bool Heap_FindMin(tHeap heap, Data_t *value) {
  (void)heap;
  (void)value;
  return false;
}

bool Heap_DeleteMin(tHeap *heap, Data_t *deleteValue) {
  (void)heap;
  (void)deleteValue;
  return false;
}
void Heap_Print(tHeap heap) { (void)heap; }

bool Heap_Empty(tHeap heap) {
  (void)heap;
  return false;
}

unsigned Heap_Count(tHeap heap) {
  (void)heap;
  return 0;
}

void Heap_Swap(tHeap *heap, unsigned index1, unsigned index2) {
  (void)heap;
  (void)index1;
  (void)index2;
}
