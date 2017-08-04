namespace Walker.Data {
	using System;
	using System.Collections;
	using System.Collections.Generic;

	public class Dictionary2D<Key1, Key2, Value> : IEnumerable<Value> {

		Dictionary<Key1, Dictionary<Key2, Value>> dict = new Dictionary<Key1, Dictionary<Key2, Value>>();

		public Dictionary<Key2, Value> this[Key1 a] {
			get => dict[a];
			set => dict[a] = value;
		}

		public Value this[Key1 a, Key2 b] {
			get => dict[a][b];
			set => dict[a][b] = value;
		}

		public void Add(Key1 a, Dictionary<Key2, Value> val) {
			dict.Add(a, val);
		}

		public bool Remove(Key1 a) {
			return dict.Remove(a);
		}

		public void Add(Key1 a, Key2 b, Value val) {
			if (!dict.ContainsKey(a)) { Add(a, new Dictionary<Key2, Value>()); }
			dict[a].Add(b, val);
		}

		public bool Remove(Key1 a, Key2 b) {
			return dict[a].Remove(b);
		}

		public IEnumerator<Value> GetEnumerator() {
			throw new NotImplementedException();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
	}
}