package farm.animals;

import farm.objects.Empty;
import farm.Farm;
import farm.Cell;
import java.util.concurrent.ThreadLocalRandom;

public class Dog extends Thread {
    private int x, y;
    private final Farm farm;
    private final int sleepTime;

    public Dog(int x, int y, Farm farm, int sleepTime) {
        this.x = x;
        this.y = y;
        this.farm = farm;
        this.sleepTime = sleepTime;
    }

    @Override
    public void run() {
        ThreadLocalRandom random = ThreadLocalRandom.current();
        while (farm.isRunning()) {
            int dx = random.nextInt(3) - 1;
            int dy = random.nextInt(3) - 1;
            if (dx != 0 || dy != 0) {
                int newX = x + dx;
                int newY = y + dy;

                Cell currentCell = farm.getCell(x, y);
                Cell newCell = farm.getCell(newX, newY);

                boolean locked = newCell.getLock().tryLock();
                if (locked) {
                    try {
                        currentCell.getLock().lock();
                        try {
                            if (newCell.getContent() instanceof Empty) {
                                newCell.setContent(this);
                                currentCell.setContent(new Empty());
                                x = newX;
                                y = newY;
                            }
                        } finally {
                            currentCell.getLock().unlock();
                        }
                    } finally {
                        newCell.getLock().unlock();
                    }
                }
            }

            try {
                Thread.sleep(sleepTime);
            } catch (InterruptedException e) {
                Thread.currentThread().interrupt();
            }
        }
    }

    @Override
    public String toString() {
        return "D";
    }
}