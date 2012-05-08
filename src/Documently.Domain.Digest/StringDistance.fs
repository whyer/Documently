module StringDistance

let ``edit distance for capitalized characters`` () =
  ()//editDistance "åäÖ" "ö" =? 2 // not 3, which would be case sensitive