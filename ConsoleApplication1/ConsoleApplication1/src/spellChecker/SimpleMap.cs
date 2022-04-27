using System;
using ikvm.extensions;

namespace spellChecker
{
    
public class SimpleMap {

    /**
     * The maximum capacity, used if a higher value is implicitly specified
     * by either of the constructors with arguments.
     * MUST be a power of two <= 1<<30.
     */
    static  int MAXIMUM_CAPACITY = 1 << 30;

    /**
     * The table, resized as necessary. Length MUST Always be a power of two.
     */
    private Entry[] table;

    /**
     * The number of key-value mappings contained in this map.
     */
    private int count;

    /**
     * The next size value at which to resize (capacity * load factor).
     * @serial
     */
    private int threshold;

    /**
     * The load factor for the hash table.
     *
     * @serial
     */
    private float loadFactor = 0.75f;

    /**
     * Constructs an empty <tt>SimpleMap</tt> with the default initial capacity
     * (16) and the default load factor (0.75).
     */
    public SimpleMap() {
        threshold = (int)(16 * loadFactor);
        table = new Entry[16];
    }

    /**
     * Applies a supplemental hash function to a given hashCode, which
     * defends against poor quality hash functions.  This is critical
     * because SimpleMap uses power-of-two length hash tables, that
     * otherwise encounter collisions for hashCodes that do not differ
     * in lower bits. Note: Null keys always map to hash 0, thus index 0.
     */
    private static int hash(int h) {
        // This function ensures that hashCodes that differ only by
        // constant multiples at each bit position have a bounded
        // number of collisions (approximately 8 at default load factor).
        //h ^= (h >> 20) ^ (h >> 12);
        return ((h >> 20) ^ (h >> 12)) ^ (h >> 7) ^ (h >> 4);
    }

    /**
     * Returns index for hash code h.
     */
    private  static int indexFor( int h,  int length) {
        return h & (length-1);
    }

    /**
     * Returns the number of key-value mappings in this map.
     *
     * @return the number of key-value mappings in this map
     */
    public int size() {
        return count;
    }

    /**
     * Returns the value to which the specified key is mapped,
     * or {@code null} if this map contains no mapping for the key.
     *
     * <p>More formally, if this map contains a mapping from a key
     * {@code k} to a value {@code v} such that {@code (key==null ? k==null :
     * key.equals(k))}, then this method returns {@code v}; otherwise
     * it returns {@code null}.  (There can be at most one such mapping.)
     *
     * <p>A return value of {@code null} does not <i>necessarily</i>
     * indicate that the map contains no mapping for the key; it's also
     * possible that the map explicitly maps the key to {@code null}.
     * The {@link #containsKey containsKey} operation may be used to
     * distinguish these two cases.
     *
     * @see #put(String, int)
     */
    public  short get( string key) {
        if (key == null)
            return (short) 0;
        var hash = SimpleMap.hash(key.hashCode());
        for (Entry e = table[indexFor(hash, table.Length)];
             e != null;
             e = e.next) {
            string k;
            if (e.hash == hash && ((k = e.getKey()) == key || key.Equals(k)))
                return (short)e.getValue();
        }
        return (short) 0;
    }

    /**
     * Returns <tt>true</tt> if this map contains a mapping for the
     * specified key.
     *
     * @param   key   The key whose presence in this map is to be tested
     * @return <tt>true</tt> if this map contains a mapping for the specified
     * key.
     */
    public  bool containsKey( string key) {
        return getEntry(key) != null;
    }

    /**
     * Returns the entry associated with the specified key in the
     * SimpleMap.  Returns null if the SimpleMap contains no mapping
     * for the key.
     */
    private  Entry getEntry( string key) {
        int hash = (key == null) ? 0 : SimpleMap.hash(key.hashCode());
        string k;
        for (Entry e = table[indexFor(hash, table.Length)];
             e != null;
             e = e.next) {
            if (e.hash == hash &&
                ((k = e.getKey()) == key || (key != null && key.Equals(k))))
                return e;
        }
        return null;
    }


    /**
     * Associates the specified value with the specified key in this map.
     * If the map previously contained a mapping for the key, the old
     * value is replaced.
     *
     * @param key key with which the specified value is to be associated
     * @param value value to be associated with the specified key
     * @return the previous value associated with <tt>key</tt>, or
     *         <tt>null</tt> if there was no mapping for <tt>key</tt>.
     *         (A <tt>null</tt> return can also indicate that the map
     *         previously associated <tt>null</tt> with <tt>key</tt>.)
     */
    public  short put( string key, short value) {
        var hash = SimpleMap.hash(key.hashCode());
        var i = indexFor(hash, table.Length);
        string k;
        for (Entry e = table[i]; e != null; e = e.next) {
            if (e.hash == hash && ((k = e.getKey()) == key || key.Equals(k))) {
                var oldValue = (short)e.getValue();
                e.setValue(value);
                return oldValue;
            }
        }

        addEntry(hash, key, value, i);
        return 0;
    }

    /**
     * Rehashes the contents of this map into a new array with a
     * larger capacity.  This method is called automatically when the
     * number of keys in this map reaches its threshold.
     *
     * If current capacity is MAXIMUM_CAPACITY, this method does not
     * resize the map, but sets threshold to Integer.MAX_VALUE.
     * This has the effect of preventing future calls.
     *
     * @param newCapacity the new capacity, MUST be a power of two;
     *        must be greater than current capacity unless current
     *        capacity is MAXIMUM_CAPACITY (in which case value
     *        is irrelevant).
     */
    private  void resize( int newCapacity) {
        Entry[] oldTable = table;
        int oldCapacity = oldTable.Length;
        if (oldCapacity == MAXIMUM_CAPACITY) {
            threshold = Int32.MaxValue;
            return;
        }

        Entry[] newTable = new Entry[newCapacity];
        transfer(newTable);
        table = newTable;
        threshold = (int)(newCapacity * loadFactor);
    }

    /**
     * Transfers all entries from current table to newTable.
     */
    private  void transfer( Entry[] newTable) {
        Entry[] src = table;
        int newCapacity = newTable.Length;
        for (int j = 0; j < src.Length; j++) {
            Entry e = src[j];
            if (e != null) {
                src[j] = null;
                do {
                    Entry next = e.next;
                    int i = indexFor(e.hash, newCapacity);
                    e.next = newTable[i];
                    newTable[i] = e;
                    e = next;
                } while (e != null);
            }
        }
    }

    private class Entry {
        public static string key;
        public static short value;
        public Entry next;
        public int hash;

        /**
         * Creates new entry.
         */
        public Entry( int h, String k,  short v,  Entry n) {
            value = v;
            next = n;
            key = k;
            hash = h;
        }

        public  string getKey() {
            return key;
        }

        public  int getValue() {
            return value;
        }

        public int setValue(short newValue) {
	    int oldValue = value;
            value = newValue;
            return oldValue;
        }

    }

    /**
     * Adds a new entry with the specified key, value and hash code to
     * the specified bucket.  It is the responsibility of this
     * method to resize the table if appropriate.
     *
     * Subclass overrides this to alter the behavior of put method.
     */
    private  void addEntry(int hash, string key, short value, int bucketIndex) {
	var e = table[bucketIndex];
        table[bucketIndex] = new Entry(hash, key, value, e);
        if (count++ >= threshold)
            resize(2 * table.Length);
    }

}

    
}