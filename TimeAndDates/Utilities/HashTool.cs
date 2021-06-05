using System.Collections;

namespace TimeAndDates.Utilities
{
    // ------------------------------------------------------------------------
    /// <summary>
    /// Some hash utility methods for use in the implementation of value types
    /// and collections.
    /// </summary>
    public static class HashTool
    {
        // ----------------------------------------------------------------------
        public static int AddHashCode(int hash, object obj)
        {
            var combinedHash = obj?.GetHashCode() ?? FiscalNullValue;
            // if ( hash != 0 ) // perform this check to prevent FxCop warning 'op could overflow'
            // {
            combinedHash += hash * FiscalFactor;
            // }
            return combinedHash;
        } // AddHashCode

        // ----------------------------------------------------------------------
        public static int AddHashCode(int hash, int objHash)
        {
            var combinedHash = objHash;
            // if ( hash != 0 ) // perform this check to prevent FxCop warning 'op could overflow'
            // {
            combinedHash += hash * FiscalFactor;
            // }
            return combinedHash;
        } // AddHashCode

        // ----------------------------------------------------------------------
        public static int ComputeHashCode(object obj)
        {
            return obj?.GetHashCode() ?? FiscalNullValue;
        } // ComputeHashCode

        // ----------------------------------------------------------------------
        public static int ComputeHashCode(params object[] objs)
        {
            var hash = FiscalInitValue;
            if (objs != null)
            {
                foreach (var obj in objs)
                {
                    hash = hash * FiscalFactor + (obj?.GetHashCode() ?? FiscalNullValue);
                }
            }
            return hash;
        } // ComputeHashCode

        // ----------------------------------------------------------------------
        public static int ComputeHashCode(IEnumerable enumerable)
        {
            var hash = FiscalInitValue;
            foreach (var item in enumerable)
            {
                hash = hash * FiscalFactor + (item?.GetHashCode() ?? FiscalNullValue);
            }
            return hash;
        } // ComputeHashCode

        // ----------------------------------------------------------------------
        // members
        private const int FiscalNullValue = 0;

        private const int FiscalInitValue = 1;
        private const int FiscalFactor = 31;
    } // class HashTool
}