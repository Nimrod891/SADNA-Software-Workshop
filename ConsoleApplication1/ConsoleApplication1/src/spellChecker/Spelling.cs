using java.io;
using java.lang;
using java.net;
using java.util;
using ArrayList = System.Collections.ArrayList;

namespace spellChecker
{


    public class Spelling
    {
		private  SimpleMap nWords = new SimpleMap();

	public Spelling() {
		try {
			//URL url = getClass().getResource("big.txt");//TODO
			var f = new File(/*url.getPath()*/"");
			var inp = new FileReader(f);
			var buffer = new char[(int)f.length()];
			inp.read(buffer);
			var begin = 0;
			var isUpper = false;
			for (var i = 0; i < buffer.Length; i++) {
				while ((('a' > buffer[i] || buffer[i] > 'z') && ('A' > buffer[i] || buffer[i] > 'Z')) && i < buffer.Length - 1 ) i++;
				begin = i;
				while ((('a' <= buffer[i] && buffer[i] <= 'z') || (isUpper = ('A' <= buffer[i] && buffer[i] <='Z'))) && i < (buffer.Length-1)){
					if(isUpper) buffer[i] = Character.toLowerCase(buffer[i]);
					i++;
				}
				var word  = new string(buffer, begin, i - begin);
				nWords.put(word, (short) (nWords.get(word) + 1));
			}
			inp.close();
		}
		catch (IOException e) {
			//Console.writeLine("file not found");
		}
	}

	private ArrayList edits(string word) { //<String>
		ArrayList result = new ArrayList();
		for(int i=0; i < word.Length; ++i) result.Add(word.Substring(0, i) + word.Substring((i+1)));
		for(int i=0; i < word.Length-1; ++i) result.Add(word.Substring(0, i) + word.Substring(i+1, i+2) + word.Substring(i, i+1) + word.Substring(i+2));
		for(int i=0; i < word.Length; ++i) for(char c='a'; c <= 'z'; ++c) result.Add(word.Substring(0, i) + String.valueOf(c) + word.Substring(i+1));
		for(int i=0; i <= word.Length; ++i) for(char c='a'; c <= 'z'; ++c) result.Add(word.Substring(0, i) + String.valueOf(c) + word.Substring(i));
		return result;
	}

	public  string correct(string word) {
		if(word == null)
			return null;
		if(word.Trim().Equals(""))
			return "";
		if(nWords.containsKey(word)) return word;
		ArrayList list = edits(word);
		IdentityHashMap candidates = new IdentityHashMap(); // k: short, v: string
		foreach(string s in list) if(nWords.containsKey(s)) candidates.put(nWords.get(s),s);
		if(candidates.size() > 0) return (string)candidates.get(Collections.max(candidates.keySet()));
		foreach(string s in list) foreach(string w in edits(s)) if(nWords.containsKey(w)) candidates.put(nWords.get(w),w);
		return (string)(candidates.size() > 0 ? candidates.get(Collections.max(candidates.keySet())) : word);
	}
    }
}