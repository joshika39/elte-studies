import javax.swing.*;
import java.awt.*;
import java.util.ArrayList;
import java.util.Arrays;

public class MapPanel extends JPanel {
    public TileHolder selectedPlayer;
    public TileHolder selectedPosition;
    public Operation currentOperation;
    public int n = 0;
    public boolean isFinished = false;

    private TileHolder[][] tiles;
    private int moves = 0;

    public int getPlayerTileCount(int id) {
        var count = 0;
        for (var row : tiles) {
            for (var holder : row) {
                if (holder.tile instanceof PlayerTile && ((PlayerTile) holder.tile).id == id) {
                    count += 1;
                }
            }
        }

        return count;
    }

    public MapPanel() {
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
            handleSelection();
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

    public void setSize(int n) {
        isFinished = false;
        moves = 0;
        selectedPlayer = null;
        selectedPosition = null;
        this.n = n;
        tiles = new TileHolder[n][n];
    }

    public void addHolder(TileHolder holder) {
        tiles[holder.y][holder.x] = holder;
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

                holder.tile.setActive((rowDiff == 1 && colDiff == 0) || (rowDiff == 0 && colDiff == 1) && holder != selectedPlayer);
            }
        }
    }

    private void performMove() {
        if (selectedPlayer == null || selectedPosition == null || isFinished) {
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
            updateColumnUp(selectedPosition.y, selectedPosition.x);
        }

        if (selectedPosition.y > selectedPlayer.y) {
            updateColumnDown(selectedPosition.y, selectedPosition.x);
        }

        moves += 1;

        if (moves >= 5 * n) {
            var player1Count = getPlayerTileCount(1);
            var player2Count = getPlayerTileCount(2);

            if (player1Count > player2Count) {
                JOptionPane.showMessageDialog(this, "Player 1 wins!");
            } else if (player2Count > player1Count) {
                JOptionPane.showMessageDialog(this, "Player 2 wins!");
            } else {
                JOptionPane.showMessageDialog(this, "Draw!");
            }

            isFinished = true;
        }

        if (getPlayerTileCount(1) == 0) {
            JOptionPane.showMessageDialog(this, "Player 2 wins!");

            isFinished = true;
        }

        if (getPlayerTileCount(2) == 0) {
            JOptionPane.showMessageDialog(this, "Player 1 wins!");

            isFinished = true;
        }

        handleSelection();
    }

    private void updateRowForward(int rowCount, int colCount) {
        var start = calculateStart(getRow(rowCount), colCount, 0, tiles[rowCount].length - 1, 1);

        var row = tiles[rowCount];
        for (int j = start - 1; j >= colCount - 1; j--) {
            var holder = row[j];
            if (j + 1 < row.length) {
                var nextHolder = row[j + 1];
                var prevNext = nextHolder.tile;
                nextHolder.changeContent(holder.tile);
                holder.changeContent(prevNext);
            } else {
                holder.clearContent();
            }
        }
    }

    private void updateRowBackward(int rowCount, int colCount) {
        var start = calculateStart(getRow(rowCount), colCount, 0, tiles[rowCount].length - 1, -1);

        var row = tiles[rowCount];
        for (int j = start + 1; j <= colCount + 1; j++) {
            var holder = row[j];
            if (j - 1 >= 0) {
                var nextHolder = row[j - 1];
                var prevNext = nextHolder.tile;
                nextHolder.changeContent(holder.tile);
                holder.changeContent(prevNext);
            } else {
                holder.clearContent();
            }
        }
    }

    private void updateColumnDown(int rowCount, int columnCount) {
        var start = calculateStart(getColumn(columnCount), rowCount, 0, tiles.length - 1, 1);

        for (int i = start - 1; i >= rowCount - 1; i--) {
            var row = tiles[i];
            var holder = row[columnCount];
            if (i + 1 < tiles.length) {
                var nextRow = tiles[i + 1];
                var nextHolder = nextRow[columnCount];
                var prevNext = nextHolder.tile;
                nextHolder.changeContent(holder.tile);
                holder.changeContent(prevNext);
            } else {
                holder.clearContent();
            }
        }
    }

    private void updateColumnUp(int rowCount, int columnCount) {
        var start = calculateStart(getColumn(columnCount), rowCount, 0, tiles.length - 1, -1);

        for (int i = start + 1; i <= rowCount + 1; i++) {
            var row = tiles[i];
            var holder = row[columnCount];
            if (i - 1 >= 0) {
                var prevRow = tiles[i - 1];
                var nextHolder = prevRow[columnCount];
                var prevNext = nextHolder.tile;

                nextHolder.changeContent(holder.tile);
                holder.changeContent(prevNext);
            } else {
                holder.clearContent();
            }

        }
    }

    private int calculateStart(ArrayList<TileHolder> targets, int currentTile, int lowerBound, int upperBound, int direction) {
        var start = currentTile;
        var reachedEmpty = false;
        while (!reachedEmpty && start >= lowerBound && start <= upperBound) {
            var holder = targets.get(start);
            if (holder.tile instanceof EmptyTile) {
                reachedEmpty = true;
            } else {
                start += direction;
            }
        }

        return start;
    }

    private ArrayList<TileHolder> getColumn(int column){
        var result = new ArrayList<TileHolder>();
        for (var row : tiles){
            result.add(row[column]);
        }

        return result;
    }

    private ArrayList<TileHolder> getRow(int row){
        return new ArrayList<>(Arrays.asList(tiles[row]));
    }
}
