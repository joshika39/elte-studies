package gyak2;

import gyak2.Point;

public class Main {
	public static void main(String[] args){
		var point = new Point(1,  5,  6);
		var volume = point.getVolume();
		System.console().printf("%d\n", volume);
	}
}
