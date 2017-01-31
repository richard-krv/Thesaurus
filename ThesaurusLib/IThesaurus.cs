using System.Collections.Generic;

namespace Thesaurus
{
    /// <summary>
    /// Represents a thesaurus.
    /// </summary>
    public interface IThesaurus
    {
        /// <summary>
        /// Adds the given synonyms to the thesaurus.
        /// </summary>
        /// <param name="synonyms">The synonyms to add.</param>
        void AddSynonyms(IEnumerable<string> synonyms);

        /// <summary>
        /// Gets the synonyms for a given word.
        /// </summary>
        /// <param name="word">The word to return the synonyms for.</param>
        /// <returns>
        /// A <see cref="IEnumerable{String}"/> of synonyms.
        /// </returns>
        IEnumerable<string> GetSynonyms(string word);
        /// <summary>
        /// Gets all words from the thesaurus.
        /// </summary>
        /// <returns>
        /// An <see cref="IEnumerable{String}"/> containing all the words in
        /// the thesaurus.
        /// </returns>
        IEnumerable<string> GetWords();
    }
}