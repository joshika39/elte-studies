package famous.sequence;

public class Fibonacci {
    public static int fib(int n) {
        return n == 1 ? 0 : n == 2 ? 1 : fib(n-1) + fib(n-2);
    }
}
