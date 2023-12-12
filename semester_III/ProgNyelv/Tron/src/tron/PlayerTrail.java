package tron;

import javax.swing.*;
import java.awt.*;

public class PlayerTrail extends JComponent {
    private Player player;
    public Player getPlayer() { return player; }
    private Position position;
    public Position getPosition() { return position; }
    private long start;
    private Color color;
    private int duration = 3000;

    public PlayerTrail(Player player, int x, int y) {
        this.player = player;
        this.position = new Position(x, y);
        this.start = System.currentTimeMillis();
        this.color = player.getColor().darker();
    }

    public PlayerTrail(Player player, int x, int y, int duration) {
        this.player = player;
        this.position = new Position(x, y);
        this.start = System.currentTimeMillis();
        this.color = player.getColor().darker();
        this.duration = duration;
    }

    public void paintComponent(Graphics2D g) {
        if (System.currentTimeMillis() - start <= duration) {
            g.setColor(color);
            g.fillRect(position.getX(), position.getY(), player.getWidth(), player.getHeight());
        }
    }
}
