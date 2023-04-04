package gyak2;
import java.text.MessageFormat;

public class rZh1 {
	public static void main(String[] args){
		var name = System.console().readLine();
		var age = Integer.parseInt(System.console().readLine());
		System.out.println(MessageFormat.format("Hello {0}, who is {1} years old", name, age));
	}
}
