using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecheartSln.Core.Scene;
using TecheartSln.Plug.Editor.Domain;

namespace TecheartSln.Plug.Editor.Scene
{
    public class QuestionEditorScence: BaseScene
    {
        public IList<Question> QuestionList { get; set; }
    }
}
