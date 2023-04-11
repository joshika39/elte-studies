package gyak1;

import gyak1.tasks.Odd;

public class Main {
	public static void main(String[] args){
		int[] nums = {0,1,2,3,4,5,6};
java.util.Arrays.stream(nums).filter( i -> i%2==0 )
                             .map( i -> i/2 )
                             .limit(3)
                             .forEach( System.out::print );
	}
}
