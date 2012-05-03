module StringDistance

/// Calcs the Damerau Levenshtein Distance (http://en.wikipedia.org/wiki/Damerau-Levenshtein_distance)
/// In information theory and computer science, the Damerau–Levenshtein distance 
/// (named after Frederick J. Damerau and Vladimir I. Levenshtein) is a "distance" 
/// (string metric) between two strings, i.e., finite sequence of symbols, given by counting 
/// the minimum number of operations needed to transform one string into the other, 
/// where an operation is defined as an insertion, deletion, or substitution of a single 
/// character, or a transposition of two adjacent characters.
/// <remarks>Source:http://www.navision-blog.de/2008/11/01/damerau-levenshtein-distance-in-fsharp-part-iii</remarks>
let calculateDL(a:'a array) (b:'a array) =

  let calcDL (a:'a array) (b: 'a array) =
    let n = a.Length + 1
    let m = b.Length + 1

    let processCell i j act l1 l2 ll1 =
      let cost =
        if a.[i-1] = b.[j-1] then 0 else 1
      let deletion = l2 + 1
      let insertion = act + 1
      let substitution = l1 + cost
      let min1 =
        deletion
        |> min insertion
        |> min substitution

      if i > 1 && j > 1 &&
        a.[i-1] = b.[j-2] && a.[i-2] = b.[j-1] then
          min min1 <| ll1 + cost
      else
        min1

    let processLine i lastL lastLastL =
      let processNext (actL,lastL,lastLastL) j =
        match actL with
          | act::actRest ->
            match lastL with
              | l1::l2::lastRest ->
                if i > 1 && j > 1 then
                  match lastLastL with
                    | ll1::lastLastRest ->
                      (processCell i j act l1 l2 ll1 :: actL,
                       l2::lastRest,
                       lastLastRest)
                    | _ -> failwith "can't be"
                else
                  (processCell i j act l1 l2 0 :: actL,
                   l2::lastRest,
                   lastLastL)
              | _ -> failwith "can't be"
          | [] -> failwith "can't be"

      let (act,last,lastLast) =
        [1..b.Length]
          |> List.fold processNext ([i],lastL,lastLastL)
      act |> List.rev

    let (lastLine,lastLastLine) =
      [1..a.Length]
        |> List.fold
            (fun (lastL,lastLastL) i -> 
               (processLine i lastL lastLastL,lastL))
            ([0..m-1],[])

    lastLine.[b.Length]

  if a.Length > b.Length then
    calcDL a b
  else
    calcDL b a