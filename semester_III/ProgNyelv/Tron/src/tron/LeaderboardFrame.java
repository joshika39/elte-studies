package tron;

import javax.swing.*;
import java.awt.*;
import java.sql.SQLException;
import java.util.ArrayList;

public class LeaderboardFrame extends JFrame {
    private HighScores highScores;
    public LeaderboardFrame() {
        super("Leaderboard");
        try {
            this.highScores = new HighScores();
        } catch (SQLException e) {
            throw new RuntimeException(e);
        }
        setSize(400, 400);
        setDefaultCloseOperation(JFrame.DISPOSE_ON_CLOSE);
        setResizable(false);
        setLocationRelativeTo(null);
        setVisible(true);
    }

    public void paintComponent(Graphics2D g) {
        g.drawString("Leaderboard", 10, 10);
        ArrayList<HighScore> highScoresList;
        try {
            highScoresList = highScores.getHighScores();
        } catch (SQLException e) {
            throw new RuntimeException(e);
        }

        for (int i = 0; i < highScoresList.size(); i++) {
            g.drawString(highScoresList.get(i).name() + ": " + highScoresList.get(i).score(), 10, 10 + (i + 1) * 10);
        }
    }
}
