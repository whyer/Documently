module StringDistanceTests

open NUnit.Framework
open StringDistance
open Swensen.Unquote.Assertions

[<Test>]
let ``simple example`` () = 
  calculateDL ("ac".ToCharArray()) ("abc".ToCharArray()) =? 1
  calculateDL ("ab".ToCharArray()) ("abcd".ToCharArray()) =? 2
  calculateDL ("ac".ToCharArray()) ("cab".ToCharArray()) =? 2
