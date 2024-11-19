package farm.animals;

import farm.Cell;
import farm.Farm;
import farm.objects.Empty;
import farm.objects.Gate;

import java.util.Random;

public class Sheep extends Thread {
    private int x, y;
    private final Farm farm;

    public Sheep(int x, int y, Farm farm) {
        this.x = x;
        this.y = y;
        this.farm = farm;
    }

    @Override
    public void run() {
        Random random = new Random();
        while (farm.isRunning()) {
            int dx = random.nextInt(3) - 1; // -1, 0, 1
            int dy = random.nextInt(3) - 1; // -1, 0, 1
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
                            if (newCell.getContent() instanceof Empty || newCell.getContent() instanceof Gate) {
                                newCell.setContent(this);
                                currentCell.setContent(new Empty());
                                x = newX;
                                y = newY;

                                if (newCell.getContent() instanceof Gate) {
                                    System.out.println("Egy juh megszökött a farmról!");
                                    farm.stopSimulation();
                                }
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
                Thread.sleep(200);
            } catch (InterruptedException e) {
                Thread.currentThread().interrupt();
            }
        }
    }

    @Override
    public String toString() {
        return "S";
    }
}
