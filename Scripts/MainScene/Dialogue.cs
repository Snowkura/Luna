using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    private List<DialogueInfo[]> dialogueInfoList;
    private int contentIndex;
    // Start is called before the first frame update
    void Start()
    {
        dialogueInfoList = new List<DialogueInfo[]>()
        {
            new DialogueInfo[]
            {
                new DialogueInfo(){name = "ルナ", content = "初めまして、あたしはルナ~~"},
                new DialogueInfo(){name = "ルナ", content = "WASDを押したらあたしを移動させることができるよ‼"},
                new DialogueInfo(){name = "ルナ", content = "先ずはHを押して操作の説明を確認しましょう"},
            },
            new DialogueInfo[]
            {
                new DialogueInfo(){name = "ナラ", content = "お久しぶり子猫ちゃん～今日も可愛いね～"},
                new DialogueInfo(){name = "ルナ", content = "お久しぶり、相変わらず元気だねー今日も商売しているの？"},
                new DialogueInfo(){name = "ナラ", content = "もちろん、いつもお金持ちに向かって頑張ってるよ！"},
                new DialogueInfo(){name = "ナラ", content = "そういえば、今日のみかちゃんは異常に騒がしいね"},
                new DialogueInfo(){name = "ルナ", content = "何かあったのかな"},
                new DialogueInfo(){name = "ナラ", content = "さあ、実は今ちょっと忙しいね、みかちゃんをなでなでしてくれるの"},
                new DialogueInfo(){name = "ルナ", content = "絶対やだ"},
                new DialogueInfo(){name = "ナラ", content = "お願い～ポーションを無料サービスであげるから"},
                new DialogueInfo(){name = "ルナ", content = "でも……あたしはネコだもん"},
                new DialogueInfo(){name = "ナラ", content = "安心して、この子今までネコを咬むことは一度もないよ、頑張って！"},
                new DialogueInfo(){name = "ルナ", content = "(しょうがない、助けてあげようか……ちょうどさき怪我した、ポーション欲しいな)"},
            },
            new DialogueInfo[]
            {
                new DialogueInfo(){name = "ナラ", content = "まだイライラしているよ"},
            },
            new DialogueInfo[]
            {
                new DialogueInfo(){name = "ナラ", content = "ありがとう子猫ちゃん、頼もしいね"},
                new DialogueInfo(){name = "ナラ", content = "あら、あそこに誰かがポーションを落ちていたね、ラッキー(*^▽^*)持って行っていいぞ～"},
                new DialogueInfo(){name = "ルナ", content = "……"},
                new DialogueInfo(){name = "ナラ", content = "遠慮せず持ってね"},
                new DialogueInfo(){name = "ルナ", content = "はあ……"},
                new DialogueInfo(){name = "ナラ", content = "実はもう一つお願いがあるんだ、聞いてくれる？"},
                new DialogueInfo(){name = "ナラ", content = "今朝は忙しく家を出て馬車の調子をじっくり見ていないので、キャンドルがバラバラに落ちたよ"},
                new DialogueInfo(){name = "ルナ", content = "(こいつ自分でしゃべり始まった)"},
                new DialogueInfo(){name = "ルナ", content = "相変わらずバカなことやってるね、仕方ない、あたしが拾ってあげる"},
                new DialogueInfo(){name = "ナラ", content = "ありがとう子猫ちゃん('ω')頑張ってね"},
            },
            new DialogueInfo[]
            {
                new DialogueInfo(){name = "ナラ", content = "まだ足りないよ"},
            },
            new DialogueInfo[]
            {
                new DialogueInfo(){name = "ナラ", content = "ありがとう！お礼としてこれあげる"},
                new DialogueInfo(){name = "ナラ", content = "そう言えば、最近この近くモンスターが増えているね"},
                new DialogueInfo(){name = "ナラ", content = "みかちゃんがイライラしているのはこいつらのせいかな(じー)"},
                new DialogueInfo(){name = "ルナ", content = "まさか……"},
                new DialogueInfo(){name = "ナラ", content = "モンスターがこんなに増えて、お姉ちゃん怖くて泣きそう(じー)"},
                new DialogueInfo(){name = "ルナ", content = "わかったよ、あたしがボコボコしてあげる、それでいいの( ｀ー´)ノ"},
                new DialogueInfo(){name = "ナラ", content = "ありがとう！五匹ぐらい倒すならお姉ちゃん安心できるね～"},
                new DialogueInfo(){name = "ナラ", content = "このままだとお客さんはどんどん減ってお財布がすっからかんになっちゃう"},
                new DialogueInfo(){name = "ルナ", content = "いま本音言っちゃったね……"},
            },
            new DialogueInfo[]
            {
                new DialogueInfo(){name = "ナラ", content = "さぼってるの？"},
                new DialogueInfo(){name = "ルナ", content = "うるさい！"},
            },
            new DialogueInfo[]
            {
                new DialogueInfo(){name = "ナラ", content = "さすが子猫ちゃん、速い！"},
                new DialogueInfo(){name = "ルナ", content = "まあ、こんなもんか"},
                new DialogueInfo(){name = "ナラ", content = "私のボディーガードになってくれない？美味しいご飯をあげるよ～"},
                new DialogueInfo(){name = "ルナ", content = "……殴るぞ"},
                new DialogueInfo(){name = "ナラ", content = "冗談冗談、さって、私もそろそろ行くね"},
                new DialogueInfo(){name = "ナラ", content = "お客さんはまだ待っているから"},
                new DialogueInfo(){name = "ルナ", content = "うん、気を付けてね"},
                new DialogueInfo(){name = "ナラ", content = "バイバイ～"},               
            },
        };
        GameManager.Instance.dialogueInfoIndex = 0;
        contentIndex = 1;
    }   

    public void DisplayDialogue()
    {
        if (GameManager.Instance.dialogueInfoIndex > 7)
        {
            GameManager.Instance.canControlLuna = true;
            return;
        }
        else
        {
            if (contentIndex >= dialogueInfoList[GameManager.Instance.dialogueInfoIndex].Length)
            {
                if (GameManager.Instance.dialogueInfoIndex == 2 && !GameManager.Instance.hasPetTheDog)
                {

                }
                else if (GameManager.Instance.dialogueInfoIndex == 4 && GameManager.Instance.candleNum < 5)
                {

                }
                else if (GameManager.Instance.dialogueInfoIndex == 6 && GameManager.Instance.monKillNum < 5)
                {

                }
                else
                {
                    if (GameManager.Instance.dialogueInfoIndex == 3)
                    {
                        GameManager.Instance.candleAll.SetActive(true);
                    }
                    else if (GameManager.Instance.dialogueInfoIndex == 5)
                    {
                        GameManager.Instance.monsterAll.SetActive(true);
                    }
                    GameManager.Instance.dialogueInfoIndex++;
                }
                contentIndex = 0;
                UIManeger.Instance.ShowDialogue();
                GameManager.Instance.canControlLuna = true;
            }
            else
            {
                DialogueInfo dialogueInfo = dialogueInfoList[GameManager.Instance.dialogueInfoIndex][contentIndex];
                UIManeger.Instance.ShowDialogue(dialogueInfo.content, dialogueInfo.name);
                contentIndex++;                
            }
        }
        
    }
}

public struct DialogueInfo
{
    public string name;
    public string content;
}
