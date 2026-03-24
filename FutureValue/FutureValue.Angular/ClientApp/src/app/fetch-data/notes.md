Here are two JavaScript functions for exact and fuzzy searching:

## Exact Match Search

```javascript
function exactSearch(array, query) {
  return array.filter(item => item === query);
}
```

This function filters the array and returns only items that match the query string exactly. It's case-sensitive, so `"hello"` won't match `"Hello"`.

---

## Fuzzy Search

```javascript
function fuzzySearch(array, query) {
  const lowerQuery = query.toLowerCase();
  
  return array
    .map(item => {
      const lowerItem = item.toLowerCase();
      let queryIndex = 0;
      let score = 0;
      
      for (let i = 0; i < lowerItem.length && queryIndex < lowerQuery.length; i++) {
        if (lowerItem[i] === lowerQuery[queryIndex]) {
          score++;
          queryIndex++;
        }
      }
      
      return { item, score, matched: queryIndex === lowerQuery.length };
    })
    .filter(result => result.matched)
    .sort((a, b) => b.score - a.score)
    .map(result => result.item);
}
```

This function performs a fuzzy search by looking for characters from the query in order within each array item. It:
- Converts both the query and items to lowercase for case-insensitive matching
- Scores each item based on how many characters matched
- Only returns items where all query characters were found in order
- Sorts results by score (highest first)

---

## Usage Examples

```javascript
const fruits = ["apple", "apricot", "banana", "blueberry", "cherry"];

// Exact search
exactSearch(fruits, "apple");  // ["apple"]
exactSearch(fruits, "app");    // []

// Fuzzy search
fuzzySearch(fruits, "app");    // ["apple", "apricot"]
fuzzySearch(fruits, "br");     // ["blueberry"]
fuzzySearch(fruits, "cherry"); // ["cherry"]
```

The fuzzy search is more forgiving—it finds matches even when the query characters aren't consecutive in the target string, making it useful for autocomplete and search features.
