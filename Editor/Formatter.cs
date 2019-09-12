using System.Text;
using UnityEngine;

namespace Active.Log{
    public static class Formatter{

    public static string State(History history)
    => history.empty ? "History is empty"
                     : history.last.Format(Time.frameCount);

    public static string Latest(History history){
        int count = 0, N = !history;
        int i = N;
        while((i > 0) && (count < Config.MaxLines))
            count += history[--i].count;
        var x = new StringBuilder();
        for(int z = i; z < N; z++){
            x.Append($"#{FrameRange(history, z)} ".PadRight(
                                            Config.LogLineLength, '-') + '\n');
            x.Append(history[z].Format(-1));
        }
        return x.ToString();
    }

    static string FrameRange(History history, int i){
        int begin = history[i].index, end = history.End(i);
        if(end==-1) throw new System.Exception("negative index");
        return begin == end ? begin.ToString() : $"{begin}-{end}";
    }

}}