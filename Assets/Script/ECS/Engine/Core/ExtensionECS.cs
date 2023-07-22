public static class ExtensionECS
{
    public static bool CompareFlags(this int[] arr, int[] arr2)
    {
        for (var i = 0; i < arr.Length; i++)
        {
            if (arr[i] != arr2[i]) return false;
        }
        return false;
    }

    public static bool ContainComponent(this int[] arr, Component component)
    {
        int intIndex = (int)component / 32;
        int bitIndex = (int)component % 32;
        return (arr[intIndex] & (1 << bitIndex)) != 0;
    }

    public static void RemoveFlag(this int[] arr, Component component)
    {
        int intIndex = (int)component / 32;
        int bitIndex = (int)component % 32;
        arr[intIndex] &= (int)(~(1 << bitIndex));
    }

    public static void AddFlag(this int[] arr, Component component)
    {
        int intIndex = (int)component / 32;
        int bitIndex = (int)component % 32;
        arr[intIndex] |= (int)(1 << bitIndex);
    }

    public static bool ContainFlag(this int val, int otherVal)
    {
        return (val | otherVal) == val;
    }

}