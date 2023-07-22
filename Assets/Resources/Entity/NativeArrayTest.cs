using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
public class NativeArrayTest : MonoBehaviour
{
    NativeArray<int> myArray;

    void Start()
    {
        // Khởi tạo NativeArray với kích thước ban đầu là 10
        myArray = new NativeArray<int>(10, Allocator.Persistent);

        // Truy xuất và gán giá trị cho các phần tử trong NativeArray
        for (int i = 0; i < myArray.Length; i++)
        {
            myArray[i] = i;
        }

        // Mở rộng NativeArray thành kích thước mới là 20
        int newLength = 20;
        if (newLength > myArray.Length)
        {
            // Sử dụng UnsafeUtility để mở rộng NativeArray
            NativeArray<int> newArray = new NativeArray<int>(newLength, Allocator.Persistent);
            unsafe
            {
                UnsafeUtility.MemCpy(newArray.GetUnsafePtr(), myArray.GetUnsafePtr(), myArray.Length * UnsafeUtility.SizeOf<int>());
            }
            myArray.Dispose(); // Giải phóng NativeArray cũ
            myArray = newArray; // Gán NativeArray mới vào biến myArray
        }
    }

    void OnDestroy()
    {
        myArray.Dispose(); // Đảm bảo giải phóng bộ nhớ của NativeArray khi không sử dụng nữa
    }
}
