package gyak7;
import java.util.*;

public class StringMap {

	public static HashMap<Integer, String> mapStrLength(String[] strings) {
		var hashMap = new HashMap<Integer, String>();
		for (String s : strings) {
			var length = s.length();
			if (!hashMap.containsKey(length)) {
				hashMap.put(length, s);
			}
		}
		return hashMap;
	}
	

    public static void main(String[] args) {
        String[] ss = {
            "a",
            "aa",
            "aaaa",
            "b",
            "aaaaa",
            "ab"
        };

        for(var e: mapStrLength(ss).entrySet()) {
            System.console().printf("(%d): %s\n", e.getKey(), e.getValue());
        }
    }
}