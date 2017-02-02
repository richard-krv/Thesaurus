using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ric.ThesaurusLib.Repositories
{
    internal class ThesaurusRepositoryFile : RepositoryBase
    {
        public ThesaurusRepositoryFile(int cancelTimeoutMilliseconds = DefaultCancelTimeoutMilliseconds)
            : base(cancelTimeoutMilliseconds)
        {
        }

        internal string DataFolderPath
        {
            get
            {
                return Path.Combine(Path.GetDirectoryName((new Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath), "data");
            }
        }
        string AllFilesWildcard = string.Format("*?.{0}", DataFileExtention);

        internal string BuildFileName(string synonymGroup)
        {
            return Path.Combine(DataFolderPath, string.Format("{0}.{1}", synonymGroup, DataFileExtention));
        }

        public override string GetNewSynonymGroupName()
        {
            var resultFileName = "";
            var existingGroupNames = GetSynonymGroupNames();
            if (existingGroupNames.Count() == 0)
                resultFileName = "1";
            else
            {
                var intNames = existingGroupNames.Where(f => IsInt32(f)).Select(v => Convert.ToInt32(v));
                resultFileName = (intNames.Count() == 0 ? 1 : intNames.Max() + 1).ToString();
            }
            return resultFileName;
        }

        public const string DataFileExtention = "json";

        internal bool IsInt32(string val)
        {
            int v = 0;
            return int.TryParse(val, out v);
        }

        public override Task<bool> IsContainedInSynonymGroupAsync(string word, string synonymGroupName)
        {
            var data = LoadSynonymGroupText(synonymGroupName);
            var match = Regex.Match(data, string.Format("{0}", word));
            //if (match.Success) CancelSrc.Cancel();
            return Task.FromResult(match.Success);
        }

        public override IEnumerable<string> GetSynonymGroupNames()
        {
            return Directory.GetFiles(DataFolderPath, AllFilesWildcard).Select(fn => Path.GetFileNameWithoutExtension(fn));
        }

        public override void SaveToPersistence(string synonymGroupName, IEnumerable<string> synonyms)
        {
            File.WriteAllLines(BuildFileName(synonymGroupName), synonyms);
        }

        internal string LoadSynonymGroupText(string synonymGroupName)
        {
            return string.Join("\n", LoadSynonymGroupWords(synonymGroupName));
        }

        public override IEnumerable<string> LoadSynonymGroupWords(string synonymGroupName)
        {
            return File.ReadAllLines(BuildFileName(synonymGroupName));
        }
    }
}
