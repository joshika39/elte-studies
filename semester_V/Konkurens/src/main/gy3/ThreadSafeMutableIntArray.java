package main.gy3;

import java.util.Arrays;

public class ThreadSafeMutableIntArray {
    private int capacity;
    private Object[] locks;
    private final int[] array;

    public ThreadSafeMutableIntArray(int capacity) {
        this.capacity = capacity;
        this.array = new int[capacity];
        this.locks = new Object[capacity];
    }

    public ThreadSafeMutableIntArray(int[] array) {
        this.capacity = array.length;
        this.array = Arrays.copyOf(array, capacity);
        this.locks = new Object[capacity];
    }

    public int get(int index) {
        synchronized (locks[index]) {
            return array[index];
        }
    }

    public void set(int index, int value) {
        synchronized (locks[index]) {
            array[index] = value;
        }
    }
}
