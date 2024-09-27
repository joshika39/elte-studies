package main.gy3;

public class ThreadSafeMutableIntArray {
    private int capacity;
    private final Object lock = new Object();
    private final int[] array;

    public ThreadSafeMutableIntArray(int capacity) {
        this.capacity = capacity;
        this.array = new int[capacity];
    }

    public int get(int index) {
        synchronized (lock) {
            return array[index];
        }
    }

    public void set(int index, int value) {
        synchronized (lock) {
            array[index] = value;
        }
    }
}
