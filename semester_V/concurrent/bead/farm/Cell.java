package farm;

import java.util.concurrent.locks.ReentrantLock;


public class Cell {
    private Object content;
    private final ReentrantLock lock = new ReentrantLock();

    public Cell(Object content) {
        this.content = content;
    }

    public Object getContent() {
        return content;
    }

    public void setContent(Object content) {
        this.content = content;
    }

    public ReentrantLock getLock() {
        return lock;
    }
}
