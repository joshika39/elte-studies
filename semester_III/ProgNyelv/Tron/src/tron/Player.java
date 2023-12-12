package tron;

import javax.swing.*;
import java.awt.*;
import java.util.ArrayList;

public class Player extends Sprite {
    private DirectionEnum direction = DirectionEnum.RIGHT;
    public DirectionEnum getDirection() { return direction; }
    public void setDirection(DirectionEnum direction) { this.direction = direction; }

    private final ArrayList<PlayerTrail> trails = new ArrayList<>();

    private String name;
    public String getName() { return name; }

    private boolean isAlive = true;
    public boolean isAlive() { return isAlive; }
    public void setAlive(boolean alive) { isAlive = alive; }

    public Player(int x, int y, int width, int height, Color color, String name) {
        super(x, y, width, height, color);
        lastPosition = new Position(x, y);
        this.name = name;
    }

    private Position lastPosition;
    
    public void move() {
        switch (direction) {
            case UP:
                position.setY(position.getY() - 1);
                break;
            case DOWN:
                position.setY(position.getY() + 1);
                break;
            case LEFT:
                position.setX(position.getX() - 1);
                break;
            case RIGHT:
                position.setX(position.getX() + 1);
                break;
        }

        // Check for a position delta movment the size of the player
        if (Math.abs(lastPosition.getX() - position.getX()) >= getWidth() / 2 || Math.abs(lastPosition.getY() - position.getY()) >= getHeight() / 2) {
            trails.add(new PlayerTrail(this, lastPosition.getX(), lastPosition.getY()));
            lastPosition = new Position(position.getX(), position.getY());
        }
    }

    public boolean checkCollision(Player other) {
        Rectangle otherRect = new Rectangle(other.position.getX(), other.position.getY(), other.getWidth(), other.getHeight());

        for (PlayerTrail trail : trails) {
            Rectangle trailRect = new Rectangle(
                    trail.getPosition().getX(),
                    trail.getPosition().getY(),
                    trail.getPlayer().getWidth(),
                    trail.getPlayer().getHeight()
            );

            if (otherRect.intersects(trailRect)) {
                return true;
            }
        }
        return false;
    }

    @Override
    public void paintComponent(Graphics2D g) {
        for (PlayerTrail trail : trails) {
            trail.paintComponent(g);
        }
        super.paintComponent(g);
    }

    @Override
    public String toString() {
        return name;
    }
}
