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
        setPreferredSize(new Dimension(400, 400));
        setDefaultCloseOperation(JFrame.DISPOSE_ON_CLOSE);


        ArrayList<HighScore> highScoresList;
        try {
            highScoresList = highScores.getHighScores();
        } catch (SQLException e) {
            throw new RuntimeException(e);
        }

        var panel = new JPanel();
        panel.setLayout(new BoxLayout(panel, BoxLayout.Y_AXIS));

        for (var highScore : highScoresList) {
            var label = new JLabel(highScore.name() + ": " + highScore.score());
            panel.add(label);
        }

        getContentPane().add(panel);

        setResizable(false);
        setVisible(true);
        pack();
    }
}
