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
