/**
 * @file       main.c
 * @author     Ondřej Ševčík
 * @date       6/2019
 * @brief      Main file for heap
 * **************************************************************
 * @par       COPYRIGHT NOTICE (c) 2019 TBU in Zlin. All rights reserved.
 */

/* Private includes -------------------------------------------------------- */
#include <stdio.h>
#include <stdlib.h>

#include "heap.h"
#include "ioutils.h"

int main() {
  tHeap heap;

  Data_t dataPerson;

  bool init = false;
  bool running = true;
  char userInput;
  printf(
      "Heap v1.0\n----------\n\n"
      "0: Heap_Init,\n"
      "1: Heap_Insert,\n"
      "2: Heap_Print,\n"
      "3: Heap_DeleteMin\n"
      "4: Heap_FindMin\n"
      "M: Menu\n"
      "E: END\n");
  while (running) {
    running = io_utils_get_char(&userInput);
    if (running) {
      printf("%c\n", userInput);
    }

    switch (userInput) {
      case '0':
        printf("Your choice: 0 - Heap_Init\n\n");
        if (!init) {
          printf("Initializing heap!\n");
          init = Heap_Init(&heap);
        } else
          printf("Heap was already initialized!\n");
        break;
      case '1':
        printf("Your choice: 1 - Heap_Insert\n\n");
        if (init) {
          Data_Get(&dataPerson);
          Heap_Insert(&heap, dataPerson);
        } else {
          printf("Initialize the heap first!\n");
        }
        break;
      case '2':
        printf("Your choice: 2 - Heap_Print\n\n");
        if (init) {
          Heap_Print(heap);
        } else {
          printf("Initialize the heap first!\n");
        }
        break;
      case '3':
        printf("Your choice: 3 - Heap_DeleteMin\n\n");
        if (init) {
          ;
          if (Heap_DeleteMin(&heap, &dataPerson)) {
            printf("Item that was deleted: ");
            Data_Print(&dataPerson);
          } else {
            printf("Heap is already empty.\n");
          }
        } else {
          printf("Initialize the heap first!\n");
        }
        break;
      case '4':
        printf("Your choice: 4 - Heap_FindMin\n\n");
        if (init) {
          printf("The lowest item in heap is: ");
          if (Heap_FindMin(heap, &dataPerson)) {
            Data_Print(&dataPerson);
          } else {
            printf("Heap is already empty.\n");
          }
        } else {
          printf("Initialize the heap first!\n");
        }
        break;
      case 'm':
      case 'M':
        printf(
            "Heap v1.0\n----------\n\n"
            "0: Heap_Init,\n"
            "1: Heap_Insert,\n"
            "2: Heap_Print,\n"
            "3: Heap_DeleteMin\n"
            "4: Heap_FindMin\n"
            "M: Menu\n"
            "E: END\n");
        break;
      case 'e':
      case 'E':
        running = false;
        break;
      default:
        printf("Unknown option!\n");
        break;
    }

    printf(
        "\n"
        "Type char 0-A, E=END, M=MENU:\n"
        "************************************************************\n");
  }

  if (init) {
    while (!Heap_Empty(heap)) {
      Heap_DeleteMin(&heap, NULL);
    }

    Heap_Destruct(&heap);
  }
  return 0;
}
