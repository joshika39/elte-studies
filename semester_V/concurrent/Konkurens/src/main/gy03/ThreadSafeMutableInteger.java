package main.gy3;

import java.util.function.IntUnaryOperator;

public class ThreadSafeMutableInteger {
    private int value;

    public ThreadSafeMutableInteger() {
        this(0);
    }

    public ThreadSafeMutableInteger(int value) {
        this.value = value;
    }

    public synchronized int get() {
        return value;
    }

    public synchronized void set(int value) {
        this.value = value;
    }


    public synchronized int getAndIncrement() {
        return value++;
    }

    public synchronized int incrementAndGet() {
        return ++value;
    }

    public synchronized int addAndGet(int value) {
        this.value += value;
        return this.value;
    }

    public synchronized int getAndAdd(int value) {
        int oldValue = this.value;
        this.value += value;
        return oldValue;
    }

    public synchronized int getAndDecrement() {
        return value--;
    }

    public synchronized int decrementAndGet() {
        return --value;
    }

    public synchronized int getAndUpdate(IntUnaryOperator updateFunction) {
        int oldValue = value;
        value = updateFunction.applyAsInt(value);
        return oldValue;
    }

    public synchronized int updateAndGet(IntUnaryOperator updateFunction) {
        value = updateFunction.applyAsInt(value);
        return value;
    }
}
