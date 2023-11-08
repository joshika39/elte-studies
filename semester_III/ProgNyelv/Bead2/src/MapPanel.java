import javax.swing.*;
import java.awt.*;
import java.util.ArrayList;

public class MapPanel extends JPanel {
    public TileHolder selectedPlayer;
    public TileHolder selectedPosition;

    public TileHolder[][] tiles;
    public int n;

    private int moves = 0;

    public Operation currentOperation;

    public MapPanel() {
        setBackground(Color.CYAN);
        setLayout(null);
    }

    public void nextOperation(TileHolder newHolder) {
        if (currentOperation == Operation.SelectPlayer) {
            selectedPlayer = newHolder;
            handleMove();
            return;
        }

        if (currentOperation == Operation.SelectMove) {
            selectedPosition = newHolder;
            performMove();
        }
    }

    public void handleSelection() {
        currentOperation = Operation.SelectPlayer;
        var player = moves % 2 == 0 ? 1 : 2;
        for (var row : tiles) {
            for (var holder : row) {
                holder.tile.setActive(holder.tile instanceof PlayerTile && ((PlayerTile) holder.tile).id == player);
            }
        }
    }

    private void handleMove() {
        if (selectedPlayer == null) {
            return;
        }

        currentOperation = Operation.SelectMove;
        for (int i = 0; i < tiles.length; i++) {
            var row = tiles[i];
            for (int j = 0; j < row.length; j++) {
                var holder = row[j];

                int rowDiff = Math.abs(i - selectedPlayer.y);
                int colDiff = Math.abs(j - selectedPlayer.x);

                if ((rowDiff == 1 && colDiff == 0) || (rowDiff == 0 && colDiff == 1) && holder != selectedPlayer) {
                    holder.tile.setActive(true);
                } else {
                    holder.tile.setActive(false);
                }
            }
        }
    }

    private void performMove() {
        if (selectedPlayer == null || selectedPosition == null) {
            return;
        }
        currentOperation = Operation.Moved;

        if (selectedPosition.x < selectedPlayer.x) {
            updateRowBackward(selectedPosition.y, selectedPosition.x);
        }

        if (selectedPosition.x > selectedPlayer.x) {
            updateRowForward(selectedPosition.y, selectedPosition.x);
        }

        if (selectedPosition.y < selectedPlayer.y) {

        }

        if (selectedPosition.y > selectedPlayer.y) {

        }

        moves += 1;
        selectedPosition.changeContent(selectedPlayer.tile);
        selectedPlayer.clearContent();
        repaint();
        revalidate();
        handleSelection();
    }

    private void updateRowForward(int rowCount, int colCount) {
        var row = tiles[rowCount];
        for (int j = colCount; j < row.length; j++) {
            var holder = row[j];
            if (j + 1 < row.length) {
                var nextHolder = row[j + 1];
                holder.changeContent(nextHolder.tile);
            }
        }
    }

    private void updateRowBackward(int rowCount, int colCount) {
        var row = tiles[rowCount];
        for (int j = colCount; j >= 0; j--) {
            var holder = row[j];
            if (j - 1 > 0) {
                var nextHolder = row[j - 1];
                holder.changeContent(nextHolder.tile);
            }
        }
    }
}
