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
    }

    public void changeContent(TileButton newTile) {
        this.tile.removeActionListener(this);
        tile = newTile;
        this.tile.addActionListener(this);
    }

    public void clearContent(){
        changeContent(new EmptyTile());
    }

    public void setFocus(boolean isFocused) {
//        tile.setBorder(new LineBorder(isFocused ? Color.ORANGE : null));
    }

    @Override
    public void actionPerformed(ActionEvent e) {
        if (e.getSource() == tile) {
            panel.nextOperation(this);
        }
    }
}
