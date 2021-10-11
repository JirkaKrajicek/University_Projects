/**
 * @file       tests.c
 * @author     Ondřej Ševčík
 * @date       6/2019
 * @brief      File for testing heap functions
 * **********************************************************************
 * @par       COPYRIGHT NOTICE (c) 2019 TBU in Zlin. All rights reserved.
 */

/* Private includes -------------------------------------------------------- */
#include <inttypes.h>
#include <string.h>

#include "../src/heap.h"
#include "minunit.h"

////////////////////////////// IMPORTANT ///////////////////////////////////////
/////////// Source repository: https://github.com/siu/minunit //////////////////
/////////// For more info consult with this page ///////////////////////////////
////////////////////////////////////////////////////////////////////////////////

MU_TEST(test_heap_init) {
  tHeap testHeap;
  mu_assert(Heap_Init(&testHeap),
            "HeapInit should return true, got false instead");
  mu_assert_int_eq(0, testHeap.count);
  mu_assert(testHeap.maxItems > 0, "Maximum items should be higher that 0");
  myFree(testHeap.array);
}

MU_TEST(test_heap_init_nulls) {
  mu_assert(!Heap_Init(NULL),
            "HeapInit should return false, got true instead.");
}

MU_TEST(test_heap_insert) {
  tHeap testHeap;
  Heap_Init(&testHeap);
  Data_t testData[5] = {{.name = "9", .age = 9},
                        {.name = "5", .age = 5},
                        {.name = "6", .age = 6},
                        {.name = "2", .age = 2},
                        {.name = "3", .age = 3}};

  for (int i = 0; i < 5; i++) {
    mu_assert(Heap_Insert(&testHeap, testData[i]),
              "Heap_Insert should return true when inserting data.");
  }

  mu_assert_string_eq("2", testHeap.array[0].name);
  mu_assert_int_eq(5, testHeap.count);
  mu_assert(testHeap.array[0].age < testHeap.array[1].age,
            "First item should have lower value than his left child.");
  mu_assert(testHeap.array[0].age < testHeap.array[2].age,
            "First item should have lower value than his right child.");

  mu_assert(testHeap.array[1].age < testHeap.array[3].age,
            "Second item should have lower value than second item.");
  mu_assert(testHeap.array[1].age < testHeap.array[4].age,
            "Second item should have lower value than second item.");

  myFree(testHeap.array);
}

MU_TEST(test_heap_insert_nulls) {
  Data_t testData = {.name = "0"};
  mu_assert(!Heap_Insert(NULL, testData),
            "HeapInsert should return false when NULL pointer is passed as "
            "argument.");
}

MU_TEST(test_heap_empty) {
  tHeap testHeap;
  Data_t testData = {.name = "John"};
  Heap_Init(&testHeap);
  mu_assert(Heap_Empty(testHeap),
            "Heap_Empty should return true, got false instead.");
  Heap_Insert(&testHeap, testData);
  mu_assert(!Heap_Empty(testHeap),
            "Heap_Empty should return false, got true instead.");
  Heap_DeleteMin(&testHeap, NULL);
  myFree(testHeap.array);
}

MU_TEST(test_heap_findmin) {
  tHeap testHeap;
  Data_t testData;
  Heap_Init(&testHeap);

  {
    Data_t data = {.name = "9", .age = 9};
    Heap_Insert(&testHeap, data);
  }
  Heap_FindMin(testHeap, &testData);
  mu_assert_string_eq("9", testData.name);
  {
    Data_t data = {.name = "5", .age = 5};
    Heap_Insert(&testHeap, data);
  }

  Heap_FindMin(testHeap, &testData);
  mu_assert_string_eq("5", testData.name);
  {
    Data_t data = {.name = "6", .age = 6};
    Heap_Insert(&testHeap, data);
  }

  Heap_FindMin(testHeap, &testData);
  mu_assert_string_eq("5", testData.name);
  {
    Data_t data = {.name = "2", .age = 2};
    Heap_Insert(&testHeap, data);
  }
  Heap_FindMin(testHeap, &testData);
  mu_assert_string_eq("2", testData.name);
  {
    Data_t data = {.name = "3", .age = 3};
    Heap_Insert(&testHeap, data);
  }
  Heap_FindMin(testHeap, &testData);
  mu_assert_string_eq("2", testData.name);
  myFree(testHeap.array);
}

MU_TEST(test_heap_deletemin) {
  tHeap testHeap;
  Data_t testData;
  Heap_Init(&testHeap);

  {
    Data_t data = {.name = "9", .age = 9};
    mu_assert(Heap_Insert(&testHeap, data),
              "Heap_Insert should return true when inserting data.");
  }

  Heap_FindMin(testHeap, &testData);
  mu_assert_string_eq("9", testData.name);
  {
    Data_t data = {.name = "5", .age = 5};
    mu_assert(Heap_Insert(&testHeap, data),
              "Heap_Insert should return true when inserting data.");
  }
  Heap_FindMin(testHeap, &testData);
  mu_assert_string_eq("5", testData.name);

  {
    Data_t data = {.name = "6", .age = 6};
    mu_assert(Heap_Insert(&testHeap, data),
              "Heap_Insert should return true when inserting data.");
  }
  Heap_FindMin(testHeap, &testData);
  mu_assert_string_eq("5", testData.name);
  {
    Data_t data = {.name = "2", .age = 2};
    mu_assert(Heap_Insert(&testHeap, data),
              "Heap_Insert should return true when inserting data.");
  }
  Heap_FindMin(testHeap, &testData);
  mu_assert_string_eq("2", testData.name);
  {
    Data_t data = {.name = "3", .age = 3};
    mu_assert(Heap_Insert(&testHeap, data),
              "HeapInsert should return true when inserting data.");
  }

  Heap_DeleteMin(&testHeap, &testData);
  mu_assert_string_eq("2", testData.name);
  mu_assert_int_eq(4, Heap_Count(testHeap));
  Heap_DeleteMin(&testHeap, &testData);
  mu_assert_string_eq("3", testData.name);
  mu_assert_int_eq(3, Heap_Count(testHeap));
  Heap_DeleteMin(&testHeap, &testData);
  mu_assert_string_eq("5", testData.name);
  mu_assert_int_eq(2, Heap_Count(testHeap));
  myFree(testHeap.array);
}

MU_TEST(test_heap_destruct) {
  tHeap testHeap;
  Heap_Init(&testHeap);

  Heap_Destruct(&testHeap);

  mu_assert(testHeap.array == NULL, "Array should be NULL");
  mu_assert(testHeap.maxItems == 0, "Maximum items should be 0");
  mu_assert(testHeap.count == 0, "Count items should be 0");
}

MU_TEST_SUITE(test_suite) {
  MU_RUN_TEST(test_heap_init);
  MU_RUN_TEST(test_heap_init_nulls);
  MU_RUN_TEST(test_heap_insert);
  MU_RUN_TEST(test_heap_insert_nulls);
  MU_RUN_TEST(test_heap_empty);
  MU_RUN_TEST(test_heap_findmin);
  MU_RUN_TEST(test_heap_deletemin);
  MU_RUN_TEST(test_heap_destruct);
}

int main(void) {
  MU_RUN_SUITE(test_suite);
  MU_REPORT();

  return 0;
}
