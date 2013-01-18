using System.Collections.Generic;
using System.Data.Entity;
using WuKeSong.Areas.Music.Models;

namespace WuKeSong.Areas.Music.Data
{
    public class MusicDBContextInitializer : DropCreateDatabaseIfModelChanges<MusicDBContext>
    { 
        protected override void Seed(MusicDBContext context)
        {
            var genres = new List<Genre>
            {
                new Genre{Title="ORCHESTRA",Description="ORCHESTRA就是管弦乐团，它分成四部分： 1弦乐，包括小提琴、大提琴等， 2铜管乐 3木管乐和 4敲击乐四组。弦乐组每种乐器有多人演奏 (竖琴除外 )，四组演奏者由一人统筹兼领导，他就是乐团的指挥。 ORCHESTRA於17世纪出现，到 18世纪因海顿和莫扎特的作品而清楚地建立模式。 19世纪加入了些新乐器，乐团人数加大。 ORCHESTRA是西方古典 /正统音乐的正宗。西方流行 /摇滚乐也经常运用 ORCHESTRA的部分或全部团员协助演出。"},
                new Genre{Title="FOLK",Description="民歌(FOLK)原本是指每个民族的传统歌曲，每个民族的先民都有他们自原始 /古代已有的歌曲，这些歌绝大部分都不知道谁是作者，而以口头传播，一传十十传百，一代传一代的传下去至今。不过今天我们所说的民歌 (FOLK)，大都是指流行曲年代的民歌 (FOLK)，所指的是主要以木结他为伴奏乐器，以自然坦率方式歌唱，唱出大家纯朴生活感受的那种歌曲。美国民歌手 WOODY GUTHRIE在五十年代的唱片可说是最早的民歌唱片录音，所以普遍被认定是现代民歌 (FOLK)的祖师。"},
                new Genre{Title="BOSSA NOVA",Description="BOSSA NOVA是种带JAZZ味道的巴西音乐，1950年代作曲家 ANTONIO CARLOS JOBIM将巴西音乐节奏与美国西岸 COOL JAZZ混合而成，柔和、舒服、轻松、懒洋洋、浪漫乃特色。每两个 BAR的第 1,4,7,11,14拍为重拍。"},
                new Genre{Title="CLASSICAL POP",Description="CLASSICAL POP是指带古典响乐味道的流行曲，多用弦乐伴奏的歌曲都可列入此类"},
                new Genre{Title="ACAPPELLA",Description="ACAPPELLA是指没有乐器伴奏的歌曲，但凡纯以人声唱的歌都是 ACAPPELLA，不过今天我们说 ACAPPELLA通常是指有多重和唱的那种唱法，连乐器伴奏都由人声唱出。 ACAPPELLA的相反是 INSTRUMENTAL，即纯音乐乐曲，任何类型的歌曲都可以以 ACAPPELLA形式唱出。"},
                new Genre{Title="WORLD MUSIC",Description="WORLD MUSIC是西方角度观点的词汇，意思指非英、美及西方民歌 /流行曲的音乐，通常指发展中地区或落後地区的传统音乐，例如非洲及南亚洲地区的音乐，有些地区如拉丁美洲的音乐，则能普及到自成一种类型。今天大家说的 WORLD MUSIC通常是指与西方音乐混和了风格的、改良了的传统地区音乐。"},
                new Genre{Title="DREAM-POP",Description="DREAM-POP是种“梦”般的流行曲，它有一种迷离的气氛，多靠SYNTHESIZERS（电子合成器）造成，加了ECHO效果的电结他也是重要的成分，歌唱部分往往很 'BREATHY'即呼吸声重，歌词也往往有梦般的诗意色彩。"},
                new Genre{Title="NEW AGE",Description="NEW AGE是种宁静、安逸、闲息的音乐，纯音乐作品占的比重较多，有歌唱的占较少。 NEW AGE可以是纯 ACOUSTIC(即以传统自发声乐器演奏)的，也可以是很电子化的，重点是营造出大自然平静的气氛或宇宙浩瀚的感觉，洗涤听者的心灵，令人心平气和。 NEW AGE很多时与音乐治疗有关，不少NEW AGE音乐说可以治病，也有不少与打坐冥想有关，这与 NEW AGE思潮哲学有莫大关系。 NEW AGE音乐通常被目为颇为中产阶级的音乐"},
                new Genre{Title="R&B",Description="R&B的全名是 Rhythm & Blues，一般译作“节奏怨曲”。广义上， R&B可视为“黑人的流行音乐”，它源於黑人的Blues音乐，是现今西行流行来和摇滚来的基础，Billboard杂志曾介定 R&B为所有黑人音乐，除了 Jazz和Blues之外，都可列作 R&B，可见 R&B的范围是多么的广泛。近年黑人音乐圈大为盛行的 Hip Hop和 Rap都源於 R&B，并且同时保存着不少 R&B成分。"},
                new Genre{Title="HOUSE",Description="HOUSE是於八十年代沿自 DISCO发展出来的跳舞音乐。 这是芝加哥的DJ玩出的音乐,他们将德国电子乐团Kraftwerk的一张唱片和电子鼓(Drum Machine)规律的节奏 及黑人蓝调歌声混音在一起,House就产生啦~一般翻译为\"浩室\"舞曲,为电子舞曲最基本的型式,4/4拍的节奏, 一拍一个鼓声,配上简单的旋律,常有高亢的女声歌唱. DISCO流行后，一些DJ将它改变，有心将DISCO变得较为不商业化， BASS和鼓变得更深沈，很多时变成了纯音乐作品，即使有歌唱部分也多数是由跳舞女歌手唱的简短句子，往往没有明确歌词。渐渐的，有人加入了LATIN(拉丁)、 REGGAE(瑞格源在西印度群岛)、 RAP(说唱)或 JAZZ(爵士)等元素，至八十年代后期， HOUSE冲出地下范围，成为芝加哥、纽约及伦敦流行榜的宠儿。为什么会叫\"House\"呢?就是说只要你有简单的录音设备,在家里都做得出这种音乐House也是电子乐中 最容易被大家所接受的.Cher唱的Believe 就是个好例子.而M-People可说是House代表团体.House舞曲在1986年开始流行后,可说是取代了Disco音乐."},
                new Genre{Title="古典",Description=""},
                new Genre{Title="布鲁斯",Description="形式简单却变化无穷，以每8或12小节为一个乐段的音乐所组成，歌词紧密，音阶中的“mi”音及“si”音降了半音。布鲁斯是爵士乐重要组成部分，同时也可以说它是现代摇滚乐的根。"},
                new Genre{Title="巴洛克",Description="巴洛克时期始于1600年，结束于1750年，是音乐史上第一个伟大时期。巴洛克本义指一种不规则的珍珠，当时是贬义，人们认为它的华丽、炫耀的风格是对文艺复兴风格的贬低。"},
                new Genre{Title="Jazz",Description="20世纪20年代由早期的拉格泰姆(Ragtime)和蓝调(Blues)吸取营养发展而成，有自由即兴的风格、令人兴奋的节奏、鲜明的切分音，是微妙而无法准确记谱的美妙音乐。"},
                new Genre{Title="Hip-Hop",Description="起源于80年代，B.P.M约在90-110拍，中文译为嘻哈。Hip-Hop前身是Rap和一点点的R&B，再加上各种磨片的音效声，从字面上来看Hip是臀部，Hop是单脚跳，Hip-Hop则是轻扭摆臀"},
                new Genre{Title="DJ",Description="全称Disc Jockey，可以理解成唱片骑士。在1981年正式从幕后转到前台，负责在各种音乐中挑选出适合的放给客人听，当时只是在玩一些叫做“Record”就是我们称之为黑胶唱片的东西。"},
                new Genre{Title="Trance",Description="迷幻舞曲,由Techno演变而来,听了会让你有\"出神\"的感觉,但还是保有舞曲的律动 ,很注重Bass的表现 ,某些听了会有\"催眠\"的效果.拍子也是以4/4拍为主."},
                new Genre{Title="Ambient",Description="听起来起伏不大,但其实一直在做改变,像是长时间的音效, 或是渐进式的音乐编排等等,常会营造出有层次的空间感,所以被称为\"情境音乐\", 且常对於生活周遭的声音做取样,如人声,汽车声,甚至是其他音乐的旋律...等等."},
                new Genre{Title="Techno",Description="Technology, 即表示\"高科技舞曲\"啦!利用电脑,合成器合成,做出一些特殊音效,这种音乐常常是许多音效组合起来的. Techno的节拍也是4/4拍,但速度较House快,且听起来具重复性,较强硬,较机械化,所以某些人称Techno为\"工业噪音\",但某些还是会注重旋律的. Techno起源于美国底特律,尝试将电子乐与黑人音乐结合,而产生了Detroit Techno. Detroit Techno通常较平缓, 不像一般的Techno那样强劲,可说是现代Techno的起源."},
                new Genre{Title="Electrophonic Music",Description="何谓Electrophonic Music(电子音乐)?随着时代的演进,音乐家有了更多制作音乐的方法.所谓电子音乐,就是以电子合成器,音乐软体,电脑等所产生的电子声响来制作音乐.电子音乐范围广泛,生活周遭常常能听到,在电影配乐,广告配乐,甚至某些国语流行歌中都有用到,不过以电子舞曲为最.很多人认为电子乐是一种冷冰冰,没有感情的音乐.其实电子乐也可融入Rock, Jazz甚至Blues等多种元素而充满情感的。电子音乐的类型也是多种多样的，包括House 、Techno、Ambient、Trance、Psychedelic Trance、 Breakbeat、 Brit-Hop、 Big-Beat、 Trip-Hop、Drum'n'Bass、 Jungle、Electro、Dub、Chill Out、 Minimalism。"},
                new Genre{Title="Britpop",Description="Britpop虽有个“ Pop”字，但其实是 Rock的一种，源於九十年代英伦，中文可译为“英式摇滚”，这是英伦乐坛对美国 Grunge潮的一个回应，主要是以乐队形式出现。不过， Britpop风格其实十分广泛，如 Oasis是结他摇滚乐队， Blur则Pop很多，而Pulp则接近Glam Rook及跳舞风格，不过他们都被列作 Britpop。"},
                new Genre{Title="Trip-Hop",Description="Trip-Hop是英伦／欧洲跳舞音乐的一种，它的名字来源是“ Trip＋ Hip Hop＝ Trip-Hop”，因为它发源自英国的Bristol,因此最早时称作\"Bristol Hip-Hop\".。由于把把Hip-Hop（其实很多音乐都是架构在Hip-Hop上的...不知啥是Hip-Hop的去看看跳街舞的人, 他们多半是用Hip-Hop音乐来跳的.）节奏变慢(有时很慢很慢),加入一些迷幻的味道,如很阴沉,肥厚的Bass,轻微但迷幻的 合成音效,或是些唱片的取样,有时可能连唱片的杂音都会被\"故意\"取样进去.所谓“ Trip”，指迷幻，氤氲的药物「旅程」，所以， Trip-Hop是种慢板的迷幻的、有 Jazz感觉的、迷糊的、带点 Hip Hop节奏的 Break beat音乐。它虽然隶属跳舞音乐类，但其迷幻迷糊特色已令它跳一般跳舞音乐所有的明确节拍特色相去很远。"},
                new Genre{Title="Gangsta Rap",Description="Gangsta Rap是 Rap的一种，以 Rap的内容多与都市罪案有关，充满暴力、色欲感受，这是反映现实的一种音乐路向。 Gangsta Rap於八十年代末期在美国兴起，音乐Rap中的强悍尖锐派，在美国大受欢迎，唱片销路甚高。而不少 Gangsta Rap乐手本身真正“参与”现实中各式罪案，部分更因而入狱甚至死亡，可说是真正反映现实兼令人触目惊心的乐种。"},
                new Genre{Title="Synth Pop",Description="Synth Pop中的 Synth，即 Synthesizer，顾名思义， Synth Pop就是“由 Sythesizers炮制出来的流行乐”，当然除 Synthesizers外还会用上其他电子乐器如电脑及鼓机等等。 Synth Pop於八十年代初期开始流行，至八十年代中开始沉寂，当年在香港也曾掀起过一阵热潮。 Synth Pop的特点是科技感强，有时会颇冰冷，歌曲多是“三分钟流行曲” (3-minutes Pop)，很多时 Synth Pop乐手会作入时打扮。"},
                new Genre{Title="CHAMBER POP",Description="CHAMBER POP是指典雅、高贵、精致的一种流行乐，它有一定的古典音乐感觉。 CHAMBER MUSIC一词来自古典音乐，中文叫\"室内乐\"，是种小组弦乐演奏曲式，气氛高雅。 CHAMBER POP於九十年代兴起，是对当时的 LO-FI及 GRUNGE的一种反应，强调优美的旋律、精致的配乐、乾净的录音，每每多用弦乐、管乐制造巴洛克时代的音乐感觉。"},
                new Genre{Title="重金属",Description="60年代末由布鲁斯和硬摇滚发展起来的一种摇滚风格，具有金属质感的吉他失真和反复的吉他即兴重复是它最主要的特征，是70年代以来摇滚乐最大的一个分支。"},
                new Genre{Title="Hard Rock",Description="20世纪60年代初由布鲁斯发展起来的一种摇滚乐风格，具有比较强烈的吉他失真，吉他比较复杂，主音吉他成分较多，布鲁斯味比较浓。硬摇滚与重金属通常不太容易区分"},
                new Genre{Title="Reggae",Description="又译为瑞格，是一种特殊的牙买加风格音乐，它从ska(一种uptempo风格的音乐)直接发展而来，但在它的风格中却有着明显的新奥尔良节奏与布鲁斯音乐的影子。"},
                new Genre{Title="Flamenco",Description="诞生于西班牙的安达鲁西亚(Andalucia)，糅合热情、奔放及凄哀等情感，并有强烈的节奏，三大灵魂是吉他、舞蹈、歌唱，还须伴有感情、节奏、气氛、灵魂。"},
                new Genre{Title="B-Side",Description="看过黑胶碟吗？正反两面都能唱，一面叫A-Side，一面叫B-Side。通常A-Side会收录比较主流的音乐，而B-Side则收一些有歌手自我风格的东西，B-Side也被引申为有个性的音乐。"},
                new Genre{Title="单曲",Description="歌手当然不能在出大碟前、巡演间隙让大家把他们从各大媒体上忘掉，所以他们会在专辑出来前，挑一两首歌来出张单曲CD，一来对市场来个投石问路，二来可以赚点零花钱"},
                new Genre{Title="Chill Out",Description="这个叫法来自跳舞俱乐部内特设的一个房间，里面主要放一些缓和平静的音乐，以便让舞客跳完舞后缓和情绪，休息一下。人们把在这种房间里放的音乐统称为Chill Out(冷却)音乐。"},
                new Genre{Title="Rap",Description="起源于60年代，B.P.M约在100-110拍，中译“说唱；饶舌”。Rap以人声的吟唱来代替音乐中旋律的部分，加上鼓的清晰浓郁节奏；朗朗上口的通俗歌词，一下子成为了年轻人的最爱。"},
                new Genre{Title="Punk",Description="歌词中传达某些叛逆思想及对生活环境、文化、社会、政治等的不满，音乐缺乏协调性无特定风格，是一种嘈杂的音乐，通常一群能将乐器弄出声音来的人就可以组一个朋克乐队。"},
                new Genre{Title="Soul",Description="灵歌的黄金时代随着1968年4月4日Martin·Luther·King 的遇刺身亡而悄然结束。灵歌的黄金时代也标志着多年来现实存在的种族隔离的结束，同一时期，人权运动也开始蓬勃发展。当然，美国人民还需要20 年、30 年或者更长的时间去消除长达300多年来种族压迫带来的深重的影响。"},
                new Genre{Title="骊歌",Description="逸诗有《骊驹》篇云：“骊驹在门，仆夫具存；骊驹在路，仆夫整驾。”客人临去歌《骊驹》，后人因而将告别之歌称之为“骊歌”。今天的《骊歌》多指一首在我国流行于20年代到40年代的学堂乐歌，又名《送别》。李叔同作词，英国人奥德维作曲。"}
            };
            genres.ForEach(x=>context.Genres.Add(x));
            context.SaveChanges();

            var music = new List<Models.Music>
            {
                new Models.Music{Title="献给爱丽丝", Author="贝多芬",Genre="古典"},
                new Models.Music{Title="G大调小步舞曲", Author="巴赫",Genre="古典"},
                new Models.Music{Title="A大调前奏曲", Author="肖邦",Genre="古典"},
                new Models.Music{Title="The Wings of Ikarus", Author="Banari",Genre="New Age"},
                new Models.Music{Title="送别", Author="李叔同",Genre="骊歌",Comment=""},
                new Models.Music{Title="Canon in D by Pachelbel", Author="George Winston",Genre="New Age"},
                new Models.Music{Title="Imagine", Author="John Lennon",Genre="rock n roll"},
                new Models.Music{Title="Hey Jude", Author="Beetles",Genre="BritRock"},
                new Models.Music{Title="青花瓷", Author="周杰伦",Genre="中国风"},
                new Models.Music{Title="I Say A Little Prayer", Author="Aretha Franklin",Genre="Soul",Comment=""},
                new Models.Music{Title="稻香", Author="周杰伦",Genre="R&B"},
                new Models.Music{Title="River flows in you", Author="Yinuma",Genre="New Age",SheetMusic="/KnowledgeBase/SheetMusic/River flows in you.pdf"},
                new Models.Music{Title="Sundial Dreams", Author="Kevin Kern",Genre="New Age",SheetMusic="/KnowledgeBase/SheetMusic/Sundial Dreams.pdf"},
                new Models.Music{Title="Fly Me To The Moon", Author="小野丽莎",Genre="BossaNova",Comment="自小生长在Bossa Nova故乡的巴西，耳濡目染之下，15岁便拿起哼唱的小野丽莎，从小就对Bossa Nova有很深的了解。89年回到日本出版第一张专辑，阳光般的笑容，微风般的吟唱，很快的小野丽莎便掳获相当多乐迷的心，也被封为“Bossa Nova女王”的美誉。"},
                new Models.Music{Title="Rolling in the Deep", Author="Adele",Genre="Soul"},
                new Models.Music{Title="Billie Jane", Author="Michael Jackson",Genre="Pop"}
            };
            music.ForEach(s => context.Music.Add(s));
            context.SaveChanges();
        }
    }
}
