module StringDistanceTests

open NUnit.Framework
open ArrayDistance
open Swensen.Unquote.Assertions

[<Test>]
let ``simple example`` () = 
  calculateDL ("ac".ToCharArray()) ("abc".ToCharArray()) =? 1
  calculateDL ("ab".ToCharArray()) ("abcd".ToCharArray()) =? 2
  calculateDL ("ac".ToCharArray()) ("cab".ToCharArray()) =? 2

[<Test>]
let ``more involved example`` () = 
  calculateDL ("Åäö".ToCharArray()) ("4ad 3".ToCharArray()) =? 5

open StringDistance