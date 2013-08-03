/*
UnTest Unity3D Unit Testing Framework Copyright (C) 2013 Andrew Fray

This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace UnTest {
    
public static class Assert {
    

    public static void IsTrue(bool condition) {
        if (condition == false) {
            throw new Exception("Assert failed");
        }
    }

    public static void IsEqual(object lhs, object rhs) {

        if (lhs == null) {
            if (rhs != null) {
                throw new Exception("Fail: NULL != " + rhs);
            }

        } else if (lhs.Equals(rhs) == false) {
            throw new Exception("Fail: " + lhs + " != " + rhs);
        }
    }

    public static void ThatThrowsException(Action lambda, Type exceptionType) {

        bool isThrown = false;

        try {
            lambda();

        } catch(Exception e) {
            isThrown = e.GetType() == exceptionType;

        } finally {
            if (isThrown == false) {
                throw new Exception("Did not get exception of type " 
                                    + exceptionType.ToString());
            }
        }
    }

    public static void IsEqualSequence<T>(IEnumerable<T> lhs, IEnumerable<T> rhs) {
        
        if (lhs == null) {
            if (rhs == null) {
                return;
            }
        } else if (lhs.SequenceEqual(rhs)) {
            return;
        }

        var lhsEnumerator = lhs == null ? null : lhs.GetEnumerator();
        var rhsEnumerator = rhs == null ? null : rhs.GetEnumerator();

        var comp = new System.Text.StringBuilder();

        comp.Append("Sequences not equal\n");
        comp.AppendFormat("{0} elements in left-hand side, {1} elements in right\n",
                          (lhs == null ? -1 : lhs.Count()), 
                          (rhs == null ? -1 : rhs.Count()));
        comp.Append("LHS | RHS\n");

        do
        {
            if (lhsEnumerator != null && lhsEnumerator.MoveNext() == false) {
                lhsEnumerator = null;
            }
            if (rhsEnumerator != null && rhsEnumerator.MoveNext() == false) {
                rhsEnumerator = null;
            }

            var lhsValue = lhsEnumerator == null ? " " : lhsEnumerator.Current.ToString();
            var rhsValue = rhsEnumerator == null ? " " : rhsEnumerator.Current.ToString();
            
            comp.AppendFormat("{0} | {1}\n", lhsValue, rhsValue);
            
        } while(lhsEnumerator != null || rhsEnumerator != null);
        

        throw new Exception(comp.ToString());

    }
}

}