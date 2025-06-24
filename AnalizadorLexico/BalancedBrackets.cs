using System;
using System.Collections.Generic;

namespace AnalizadorLexico {
    public class BalancedBrackets {
        public class stack {
            public int top = -1;
            public char[] items = new char[100];

            public void push(char x) {
                if (top == 99) {
                    Console.WriteLine("Stack full");
                }
                else {
                    items[++top] = x;
                }
            }

            char pop() {
                if (top == -1) {
                    Console.WriteLine("Underflow error");
                    return '�';
                }
                else {
                    char element = items[top];
                    top--;
                    return element;
                }
            }

            Boolean isEmpty() {
                return (top == -1) ? true : false;
            }
        }


        static Boolean isMatchingPair(char character1,
                                      char character2) {
            if (character1 == '(' && character2 == ')')
                return true;
            else if (character1 == '{' && character2 == '}')
                return true;
            else if (character1 == '[' && character2 == ']')
                return true;
            else
                return false;
        }


        public Boolean areBracketsBalanced(char[] exp) {

            Stack<char> st = new Stack<char>();

            for (int i = 0; i < exp.Length; i++) {

                if (exp[i] == '{' || exp[i] == '('
                    || exp[i] == '[')
                    st.Push(exp[i]);

                if (exp[i] == '}' || exp[i] == ')'
                    || exp[i] == ']') {

     
                    if (st.Count == 0) {
                        return false;
                    }

                    else if (!isMatchingPair(st.Pop(),
                                             exp[i])) {
                        return false;
                    }
                }
            }



            if (st.Count == 0)
                return true;
            else {
               
                return false;
            }
        }
    }
}