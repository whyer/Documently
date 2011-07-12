#light

namespace Documently.Indexer

open System
open System.Collections.Generic
open System.IO

module Helpers =
  let stopWordsList = "stopwords.txt"
  let svmWordsList = "svmwords.txt"
  
  let stopWords = use reader = new StreamReader(stopWordsList)
                  let stopWordsDict = new Dictionary<string, bool>()
                  while not reader.EndOfStream do
                      let word = reader.ReadLine()
                      if not (stopWordsDict.ContainsKey(word)) then
                          stopWordsDict.Add(word, true)
                  done
                  stopWordsDict 
                  
  let svmWords = use reader = new StreamReader(svmWordsList)
                 let words = new Dictionary<string, bool>()
                 while not reader.EndOfStream do
                      let word = reader.ReadLine()
                      if not (words.ContainsKey(word)) then
                          words.Add(word, true)
                      done
                 words
                 
        
  let isAlpha (str: string) = let mutable fail = false
                              for i = 0 to str.Length-1 do
                                  if not (Char.IsLetter(str.[i])) then
                                      fail <- true
                              not (fail)
                                  
  let dictionaryToList (dict: Dictionary<_,_>) = 
      let list = new List<KeyValuePair<_,_>>()
      dict |> Seq.iter (fun x -> list.Add(x))
      list
      
  let dictionaryToArray (dict: Dictionary<_,_>) = 
      let list = new List<KeyValuePair<_,_>>()
      dict |> Seq.iter (fun x -> list.Add(x))
      list.ToArray()
  
  let listToGenericList(l: 'a list) = 
      let mylist = new List<'a>()
      l |> List.iter (fun x -> mylist.Add(x))
      mylist
      
  
  let floatToBool (f: float) = 
      if (f = 0.0) then
          false
      else
          true
      
  let updateLocalDictionary (dict: Dictionary<string, (int * double)>) (word: string) = 
      if dict.ContainsKey(word) then
          let (a, b) = dict.[word]
          dict.[word] <- ((a + 1), b)
      else
          dict.Add(word, (1, 0.0))
          
  let parseDateTime (str: string) = 
      let tempDateTime = DateTime.Now
      if DateTime.TryParse(str, ref tempDateTime) then
          DateTime.Parse(str)
      else
          DateTime.MinValue
  
  let dictFreq (dict: Dictionary<string, (int * double)>) (keys: string list) = 
      keys |> List.map (fun key -> if (dict.ContainsKey(key)) then
                                      let (_, tfidf) = dict.[key]
                                      tfidf
                                   else 
                                      0.0)
           |> List.sum
                                    
  let updateGlobalDictionary (dict: Dictionary<string, int>) (word: string) =
      if dict.ContainsKey(word) then
          dict.[word] <- dict.[word] + 1
      else
          dict.Add(word, 1)
  
  let take (num: int) (mylist: 'a list) =
      if (mylist.Length < num) then
          mylist
      else
          let accumulator = new List<'a>()
          for i = 0 to num-1 do 
              accumulator.Add(mylist.[i])
          Seq.toList accumulator
                 
  
  let inBetween (source: string) (start: string) (endStr: string) = 
      if not (source.Contains(start)) || not (source.Contains(endStr)) then
          ""
      else
          let s = source.IndexOf(start)
          let e = source.IndexOf(endStr, (s+ start.Length))
          source.Substring(s+start.Length, e-(s+start.Length))
  
  
  let consoleLockObj = new System.Object()
  
  let greenConsoleWrite (message: string) = 
      // we need to lock, because there's a race condition in multithreaded calls
      lock consoleLockObj (fun () -> let old = Console.ForegroundColor
                                     Console.ForegroundColor <- ConsoleColor.Green
                                     Console.Write(message)
                                     Console.ForegroundColor <- old)
                                     
  let greenConsole (message: string) = 
      greenConsoleWrite (message + "\r\n")
  
  let redConsoleWrite (message: string) = 
      // we need to lock, because there's a race condition in multithreaded calls
      lock consoleLockObj (fun () -> let old = Console.ForegroundColor
                                     Console.ForegroundColor <- ConsoleColor.Red
                                     Console.Write(message)
                                     Console.ForegroundColor <- old)  
                                     
  let redConsole (message: string) = 
      redConsoleWrite (message + "\r\n")  