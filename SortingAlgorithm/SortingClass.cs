using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SortingAlgorithms
{
    struct SortingClass<K> where K : IEquatable<K>, IComparable<K>
    {
        //Select Sort O(n^2) time complexity -non stable, in place-
        //Insert Sort O(n^2) time complexity  ,if list is already sorted and a bit distorted, O(n) in such case
        //-Insert Sort is stable and in place-
        //Shell Sort  O(n^(3/2)) time complexity - in place, non stable-
        //Merge Sort O(nlogn) time complexity -stable , not in place -
        //Quick Sort O(nlogn) time complexity , O(n^2) in worst case! this sorting technique is used for the most of lists in compilers
        //-Quick Sort is non stable and in place-
        //Radix Sort O(n) time complexity   very slow!
        //Heap Sort O(nlogn) time complexity - non stable , in place-
        
        public void InsertionSort(K[] array) //Sıralanmış listelerde indeks bozulmalarını düzeltmede etkili bir yöntemdir.
        {
            if (array == null)
                throw new ArgumentNullException("array");

            for (int i = 1; i < array.Length; i++)
            {
                K v = array[i];
                for (int j = i - 1; j >= 0; j--)
                {
                    if (array[j].CompareTo(v) <= 0) //Worst case time complexity O(n^2)
                    {                               //Best case time complexity O(n), for already sorted list
                        break;
                    }

                    array[j + 1] = array[j];
                    array[j] = v;
                }
            }
        }
        
        public K[] InsertionSortParams(params K[] array)
        {
            if (array == null)
                throw new ArgumentNullException("array");

            for (int i = 1; i < array.Length; i++)
            {
                K v = array[i];
                for (int j = i - 1; j >= 0; j--)
                {
                    if (array[j].CompareTo(v) <= 0)
                    {
                        break;
                    }

                    array[j + 1] = array[j];
                    array[j] = v;
                }
            }

            return array;
        }
        
        public K BinarySearch(K value, params K[] array)
        {
            if (array == null)
                throw new ArgumentNullException("array");

            Array.Sort(array);
            int size = array.Length;
            int middleElement = 0, left = 0, right = array.Length-1;

            while (left<=right)
            {
                middleElement = (left + right) / 2;

                if (array[middleElement].CompareTo(value) < 0)
                {
                    left = middleElement+1;
                }
                else if(array[middleElement].CompareTo(value) > 0)
                {
                    right = middleElement-1;
                }
                else
                {
                    return array[middleElement];
                }
            }

            return default;

        }
        
        public void BubbleSort(params K[] array)
        {
            if (array == null)
                throw new ArgumentNullException("array");
            int repeat = array.Length - 1;
            for (int i = 0; i < repeat; i++)
            {
                int checkFlag = 0;
                for (int j = 0; j < repeat-i; j++)
                {
                    if (array[j].CompareTo(array[j + 1]) > 0)
                    {
                        SwapSort(j, j + 1, array);
                        checkFlag = 1;
                    }
                }

                if (checkFlag == 0) break;
            }

            
        }//O(n^2) worst case time complexity

        public void ShellSort(K[] array)
        {
            if (array == null)
                throw new ArgumentNullException("array");

            int n = array.Length;
            for (int gap = n / 2; gap >= 1; gap /= 2)
            {
                for (int k = gap; k < n; k++)
                {
                    for (int t = k - gap; t >= 0; t -= gap)
                    {
                        if (array[t + gap].CompareTo(array[t]) > 0)
                        {
                            break;
                        }
                        else
                        {
                            SwapSort(t, t + gap, array);
                        }
                    }
                }
            }
        }
        
        public void QuickSort(K[] arr, int left_index, int right_index)
        {
            if (arr == null)
                throw new ArgumentNullException("array");

            K pivot;
            int left, right;
            if (left_index >= right_index) return;

            left = left_index;
            right = right_index;

            pivot = arr[(left_index + right_index) / 2];

            while (left <= right)
            {
                while (arr[left].CompareTo(pivot) < 0) left++;
                while (arr[right].CompareTo(pivot) > 0) right--;

                if (left <= right)
                {
                    SwapSort(left, right, arr);
                    left++;
                    right--;
                }
            }

            QuickSort(arr, left_index, right);
            QuickSort(arr, left, right_index);

        }
        
        public K[] MWayMergingSort(K[] firstArray,K[] secondArray)
        {
            if(firstArray==null || secondArray==null)
                throw new ArgumentNullException("firstArray or secondArray is null");

            Array.Sort(firstArray);
            Array.Sort(secondArray);

            K[] fetchArray=new K[firstArray.Length+secondArray.Length];
            int i = 0, j = 0, k = 0;

            while (i<firstArray.Length && j<secondArray.Length)
            {
                if (firstArray[i].CompareTo(secondArray[j])>0)
                {
                    fetchArray[k] = secondArray[j];
                    j++;
                }
                else
                {
                    fetchArray[k] = firstArray[i];
                    i++;
                }

                k++;
            }

            if (i == firstArray.Length)
            {
                for (int l = j; l < secondArray.Length; l++)
                {
                    fetchArray[k] = secondArray[l];
                    k++;
                }
            }
            else 
            {
                for (int t = i; t < firstArray.Length; t++)
                {
                    fetchArray[k] = firstArray[t];
                    k++;
                }
            }

            return fetchArray;

        }
        
        public K[] MWayMergingSortGeneric(IEnumerable<K> firstArray, IEnumerable<K> secondArray)
        {
            if (firstArray == null || secondArray == null)
                throw new ArgumentNullException("firstArray or secondArray is null");

            Array.Sort(firstArray.ToArray());
            Array.Sort(secondArray.ToArray());

            K[] fetchArray = new K[firstArray.Count() + secondArray.Count()];
            int i = 0, j = 0, k = 0;

            while (i < firstArray.Count() && j < secondArray.Count())
            {
                if (firstArray.ElementAt(i).CompareTo(secondArray.ElementAt(j)) > 0)
                {
                    fetchArray[k] = secondArray.ElementAt(j);
                    j++;
                }
                else
                {
                    fetchArray[k] = firstArray.ElementAt(i);
                    i++;
                }

                k++;
            }

            if (i == firstArray.Count())
            {
                for (int l = j; l < secondArray.Count(); l++)
                {
                    fetchArray[k] = secondArray.ElementAt(l);
                    k++;
                }
            }
            else
            {
                for (int t = i; t < firstArray.Count(); t++)
                {
                    fetchArray[k] = firstArray.ElementAt(t);
                    k++;
                }
            }

            return fetchArray;

        }
        
        public void MergeSort(K[] arr, int lower_bound, int upper_bound)
        {
            if (arr == null)
                throw new ArgumentNullException("array");

            if (lower_bound < upper_bound)
            {
                int midIndex = (lower_bound + upper_bound) / 2;
                MergeSort(arr, lower_bound, midIndex);
                MergeSort(arr, midIndex + 1, upper_bound);
                Merge(arr, lower_bound, midIndex, upper_bound);
            }
        }
        
        private void Merge(K[] arr, int lowerBound, int midIndex, int upperBound)
        {
            int i = lowerBound, j = midIndex + 1, k = lowerBound;
            K[] tempArr = new K[upperBound + 1];

            while (i <= midIndex && j <= upperBound)
            {
                if (arr[i].CompareTo(arr[j]) <= 0)
                {
                    tempArr[k] = arr[i];
                    i++;
                }
                else
                {
                    tempArr[k] = arr[j];
                    j++;
                }

                k++;
            }

            if (i > midIndex)
            {
                while (j <= upperBound)
                {
                    tempArr[k] = arr[j];
                    j++;
                    k++;
                }
            }
            else
            {
                while (i <= midIndex)
                {
                    tempArr[k] = arr[i];
                    i++;
                    k++;
                }
            }

            for (int t = lowerBound; t <= upperBound; t++)
            {
                arr[t] = tempArr[t];
            }
        }
        
        private void SwapSort(int x, int y, K[] array)
        {
            if (array == null)
                throw new ArgumentNullException("array");

            K temp = array[x];
            array[x] = array[y];
            array[y] = temp;
        }

    }
}
