package gyak1.tasks;
import java.text.MessageFormat;

public class Skip {
	public static void main(String[] args) {
		for (int i = 1; i <= 4; i++) {
			System.out.println(MessageFormat.format("asd {0} asdasd {1}", 2, 3.4));
		}
	}
}