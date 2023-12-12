package tron;

import javax.swing.*;
import java.awt.*;

public class Sprite extends JComponent {

    protected Position position;

    protected int width;
    public int getWidth() { return width; }

    protected int height;
    public int getHeight() { return height; }

    protected Color color;
    public Color getColor() { return color; }

    public Sprite(int x, int y, int width, int height, Color color) {
        super();
        position = new Position(x, y);
        this.width = width;
        this.height = height;
        this.color = color;
    }

    public void paintComponent(Graphics2D g) {
        g.setColor(color);
        g.fillRect(position.getX(), position.getY(), width, height);
    }
}
