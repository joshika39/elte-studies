package tron;

import java.sql.*;
import java.util.ArrayList;
import java.util.Properties;

public class HighScores {
    PreparedStatement insertStatement;
    PreparedStatement deleteStatement;
    PreparedStatement getStatement;
    Connection connection;

    public HighScores() throws SQLException {
        Properties connectionProps = new Properties();
        connectionProps.put("user", "tronclient");
        connectionProps.put("password", "tron");
        connectionProps.put("serverTimezone", "UTC");
        String dbURL = "jdbc:mysql://localhost:3306/tronclient";
        connection = DriverManager.getConnection(dbURL, connectionProps);

        String insertQuery = "INSERT INTO results (name, score) VALUES (?, ?)";
        insertStatement = connection.prepareStatement(insertQuery);
        String deleteQuery = "DELETE FROM results WHERE name=?";
        deleteStatement = connection.prepareStatement(deleteQuery);
        String getQuery = "SELECT * FROM results WHERE name=?";
        getStatement = connection.prepareStatement(getQuery);
    }

    public ArrayList<HighScore> getHighScores() throws SQLException {
        String query = "SELECT * FROM results";
        ArrayList<HighScore> highScores = new ArrayList<>();
        Statement stmt = connection.createStatement();
        ResultSet results = stmt.executeQuery(query);
        while (results.next()) {
            String name = results.getString("name");
            int score = results.getInt("SCORE");
            highScores.add(new HighScore(name, score));
        }
        sortHighScores(highScores);
        return highScores;
    }

    public int getScore(String name) throws SQLException {
        getStatement.setString(1, name);
        ResultSet results = getStatement.executeQuery();
        if (results.next()) {
            return results.getInt("score");
        }
        return 0;
    }

    public void putHighScore(String name, int score) throws SQLException {
        deleteScores(name);
        insertScore(name, score);
    }

    /**
     * Sort the high scores in descending order.
     * @param highScores high scores to sort
     */
    private void sortHighScores(ArrayList<HighScore> highScores) {
        highScores.sort((t, t1) -> t1.score() - t.score());
    }

    private void insertScore(String name, int score) throws SQLException {
        Timestamp ts = new Timestamp(System.currentTimeMillis());
        insertStatement.setString(1, name);
        insertStatement.setInt(2, score);
        insertStatement.executeUpdate();
    }

    private void deleteScores(String  name) throws SQLException {
        deleteStatement.setString(1, name);
        deleteStatement.executeUpdate();
    }
}