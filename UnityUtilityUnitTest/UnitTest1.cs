using System;
using Xunit;
using UnityUtility;
using System.Collections.Generic;
using UnityUtility.Linq;

namespace UnityUtilityUnitTest
{
    public class DictionaryExtensionsTest
    {
        private Dictionary<int, string> m_dic1 = new Dictionary<int, string>()
        {
            {1, "a" },
            {2, "b" },
            {3, "c" }
        };
        private Dictionary<int, string> m_dic2 = new Dictionary<int, string>()
        {
            {1, "A" },
            {3, "C" },
            {5, "E" }
        };
        private Dictionary<int, string> m_empty = new Dictionary<int, string>();

        [Fact]
        public void DictionaryZipTest1()
        {
            var ziped1 = m_dic1.DictionaryZip(m_dic2, (s1, s2) => s1 + s2).GetEnumerator();
            var ziped2 = m_dic2.DictionaryZip(m_dic1, (s2, s1) => s2 + s1).GetEnumerator();
            var emptyZiped = m_empty.DictionaryZip(m_dic1, (emp, s1) => emp + s1).GetEnumerator();
            var zipedEmpty = m_dic1.DictionaryZip(m_empty, (s1, emp) => s1 + emp).GetEnumerator();

            //dic1+dic2
            ziped1.MoveNext().IsTrue();
            ziped1.Current.Is(new KeyValuePair<int, string>(1, "aA"));
            ziped1.MoveNext().IsTrue();
            ziped1.Current.Is(new KeyValuePair<int, string>(3, "cC"));
            ziped1.MoveNext().IsFalse();

            //dic2+dic1
            ziped2.MoveNext().IsTrue();
            ziped2.Current.Is(new KeyValuePair<int, string>(1, "Aa"));
            ziped2.MoveNext().IsTrue();
            ziped2.Current.Is(new KeyValuePair<int, string>(3, "Cc"));
            ziped2.MoveNext().IsFalse();

            //empty+dic1
            emptyZiped.MoveNext().IsFalse();

            //zip1+empty
            zipedEmpty.MoveNext().IsFalse();
        }

        [Fact]
        public void DictionatyCombineTest()
        {
            var combine1 = m_dic1.DictionaryCombine(m_dic2, (s1, s2) => s1 + s2).GetEnumerator();
            var combine2 = m_dic2.DictionaryCombine(m_dic1, (s2, s1) => s2 + s1).GetEnumerator();
            var emptyCombine = m_empty.DictionaryCombine(m_dic1, (emp, s1) => emp + s1).GetEnumerator();
            var combineEmpty = m_dic1.DictionaryCombine(m_empty, (s1, emp) => s1 + emp).GetEnumerator();

            combine1.MoveNext().IsTrue();
            combine1.Current.Is(new KeyValuePair<int, string>(1, "aA"));
            combine1.MoveNext().IsTrue();
            combine1.Current.Is(new KeyValuePair<int, string>(2, "b"));
            combine1.MoveNext().IsTrue();
            combine1.Current.Is(new KeyValuePair<int, string>(3, "cC"));
            combine1.MoveNext().IsTrue();
            combine1.Current.Is(new KeyValuePair<int, string>(5, "E"));
            combine1.MoveNext().IsFalse();

            combine2.MoveNext().IsTrue();
            combine2.Current.Is(new KeyValuePair<int, string>(1, "Aa"));
            combine2.MoveNext().IsTrue();
            combine2.Current.Is(new KeyValuePair<int, string>(3, "Cc"));
            combine2.MoveNext().IsTrue();
            combine2.Current.Is(new KeyValuePair<int, string>(5, "E"));
            combine2.MoveNext().IsTrue();
            combine2.Current.Is(new KeyValuePair<int, string>(2, "b"));
            combine2.MoveNext().IsFalse();

            emptyCombine.MoveNext().IsTrue();
            emptyCombine.Current.Is(new KeyValuePair<int, string>(1, "a"));
            emptyCombine.MoveNext().IsTrue();
            emptyCombine.Current.Is(new KeyValuePair<int, string>(2, "b"));
            emptyCombine.MoveNext().IsTrue();
            emptyCombine.Current.Is(new KeyValuePair<int, string>(3, "c"));
            emptyCombine.MoveNext().IsFalse();

            combineEmpty.MoveNext().IsTrue();
            combineEmpty.Current.Is(new KeyValuePair<int, string>(1, "a"));
            combineEmpty.MoveNext().IsTrue();
            combineEmpty.Current.Is(new KeyValuePair<int, string>(2, "b"));
            combineEmpty.MoveNext().IsTrue();
            combineEmpty.Current.Is(new KeyValuePair<int, string>(3, "c"));
            combineEmpty.MoveNext().IsFalse();
        }
    }
}
