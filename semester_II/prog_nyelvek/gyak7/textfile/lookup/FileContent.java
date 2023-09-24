package gyak7.textfile.lookup;

import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;

public class FileContent {
	public static int contentFileCount(String filename, String searchString){
        int count = 0;

        try (BufferedReader br = new BufferedReader(new FileReader(filename))) {
            String line;
            while ((line = br.readLine()) != null) {
                if (line.contains(searchString)) {
                    count++;
                }
            }
        } catch (IOException e) {
            e.printStackTrace();
        }

		return count;
	}
}
