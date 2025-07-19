using System.Collections;
using System.Runtime.InteropServices;

namespace NativeLibrary.Utils
{
    public class NativeList<T> : INativeList<T> where T : struct
    {
        private readonly T[] _managedArray;
        private readonly int _size;

        public NativeList(IntPtr data, int size)
        {
            _size = size;
            _managedArray = new T[size];

            int structSize = Marshal.SizeOf<T>();

            for (int i = 0; i < size; i++)
            {
                IntPtr ptr = IntPtr.Add(data, i * structSize);
                _managedArray[i] = Marshal.PtrToStructure<T>(ptr);
            }
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= _size)
                    throw new ArgumentOutOfRangeException(nameof(index));
                return _managedArray[index];
            }
        }

        public int Count => _size;

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_managedArray).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
