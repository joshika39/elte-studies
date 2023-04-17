package gyak2;

public class Point {
	public int a, b, c;

	public int getVolume() {
		return a * b * c;
	}
	
	public Point(int a, int b, int c) {
		this.a = a;
		this.b = b;
		this.c = c;
	}
}
