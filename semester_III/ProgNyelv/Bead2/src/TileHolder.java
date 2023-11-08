import javax.swing.*;
import javax.swing.border.LineBorder;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

public class TileHolder implements ActionListener {
    public TileButton tile;
    public final int x;
    public final int y;
    private final MapPanel panel;

    public TileHolder(TileButton tile, int x, int y, MapPanel panel) {
        this.tile = tile;
        this.x = x;
        this.y = y;
        this.panel = panel;
        this.tile.addActionListener(this);
        this.tile.setBounds(this.x * 30, this.y * 30, 30, 30);
        panel.add(this.tile);
    }

    public void changeContent(TileButton newTile) {
        this.tile.removeActionListener(this);
        tile = newTile;

        this.tile.addActionListener(this);
        this.tile.setBounds(this.x * 30, this.y * 30, 30, 30);
    }

    public void setContent(TileButton newTile) {
        if(tile != null) {
            this.tile.removeActionListener(this);
            panel.remove(this.tile);
        }

        tile = newTile;
        this.tile.addActionListener(this);
        this.tile.setBounds(this.x * 30, this.y * 30, 30, 30);
        panel.add(this.tile);
    }

    public void clearContent(){
        setContent(new EmptyTile());
    }

    @Override
    public void actionPerformed(ActionEvent e) {
        if (e.getSource() == tile) {
            panel.nextOperation(this);
        }
    }

    @Override
    public String toString() {
        return tile.displayedName();
    }
}
