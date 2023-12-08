public class Day7
{
    public static string Part1()
    {
        List<(Hand, long)> hands = ParseInput();
        hands.Sort();

        long totalWinnings = 0;
        for (int i = 0; i < hands.Count; i++)
        {
            totalWinnings += (i + 1) * hands[i].Item2;
        }
        return totalWinnings.ToString();
    }

    public static string Part2()
    {
        List<(JHand, long)> hands = ParseInputJ();
        hands.Sort();

        long totalWinnings = 0;
        for (int i = 0; i < hands.Count; i++)
        {
            totalWinnings += (i + 1) * hands[i].Item2;
        }
        return totalWinnings.ToString();
    }

    private static List<(Hand, long)> ParseInput()
    {
        List<(Hand, long)> hands = [];
        foreach (string line in input.Split("\n"))
        {
            string[] splitLine = line.Split(" ");
            Hand hand = new(splitLine[0]);
            long bid = long.Parse(splitLine[1]);
            hands.Add((hand, bid));
        }
        return hands;
    }

    private static List<(JHand, long)> ParseInputJ()
    {
        List<(JHand, long)> hands = [];
        foreach (string line in input.Split("\n"))
        {
            string[] splitLine = line.Split(" ");
            JHand hand = new(splitLine[0]);
            long bid = long.Parse(splitLine[1]);
            hands.Add((hand, bid));
        }
        return hands;
    }

    private class Hand : IComparable<Hand>
    {
        public Hand(string cards)
        {
            this.cards = cards;
            List<int> cardCounts = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
            foreach (char card in cards)
            {
                cardCounts[cardOrder.IndexOf(card)]++;
            }
            if (cardCounts.Any(cardCount => cardCount == 5))
            {
                handType = HandType.Five;
            }
            else if (cardCounts.Any(cardCount => cardCount == 4))
            {
                handType = HandType.Four;
            }
            else if (cardCounts.Any(cardCount => cardCount == 3))
            {
                if (cardCounts.Any(cardCount => cardCount == 2))
                {
                    handType = HandType.Full;
                }
                else
                {
                    handType = HandType.Three;
                }
            }
            else if (cardCounts.Count(cardCount => cardCount == 2) == 2)
            {
                handType = HandType.TwoPair;
            }
            else if (cardCounts.Any(cardCount => cardCount == 2))
            {
                handType = HandType.Pair;
            }
            else
            {
                handType = HandType.High;
            }
        }

        public readonly string cards;
        private readonly HandType handType;

        public int CompareTo(Hand? other)
        {
            if (other is null)
            {
                return 1;
            }

            int cmp = handType.CompareTo(other.handType);
            int i = 0;
            while (cmp == 0 && i < 5)
            {
                cmp = cardOrder.IndexOf(cards[i]).CompareTo(cardOrder.IndexOf(other.cards[i]));
                i++;
            }
            return cmp;
        }

        private const string cardOrder = "23456789TJQKA";

        private enum HandType
        {
            High,
            Pair,
            TwoPair,
            Three,
            Full,
            Four,
            Five,
        }
    }

    private class JHand : IComparable<JHand>
    {
        public JHand(string cards)
        {
            this.cards = cards;
            List<int> cardCounts = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
            foreach (char card in cards)
            {
                cardCounts[cardOrder.IndexOf(card)]++;
            }

            int jokers = cardCounts[0];
            cardCounts = cardCounts[1..];
            if (cardCounts.Any(cardCount => cardCount + jokers == 5))
            {
                handType = HandType.Five;
            }
            else if (cardCounts.Any(cardCount => cardCount + jokers == 4))
            {
                handType = HandType.Four;
            }
            else if (cardCounts.Any(cardCount => cardCount + jokers == 3))
            {
                int max = cardCounts.Max();
                int indexOf3 = cardCounts.IndexOf(max);
                if (cardCounts[0..indexOf3].Concat(cardCounts[(indexOf3 + 1)..]).Any(cardCount => cardCount == 2))
                {
                    handType = HandType.Full;
                }
                else
                {
                    handType = HandType.Three;
                }
            }
            else if (cardCounts.Count(cardCount => cardCount == 2) == 2)
            {
                handType = HandType.TwoPair;
            }
            else if (cardCounts.Any(cardCount => cardCount + jokers == 2))
            {
                handType = HandType.Pair;
            }
            else
            {
                handType = HandType.High;
            }
        }

        public readonly string cards;
        private readonly HandType handType;

        public int CompareTo(JHand? other)
        {
            if (other is null)
            {
                return 1;
            }

            int cmp = handType.CompareTo(other.handType);
            int i = 0;
            while (cmp == 0 && i < 5)
            {
                cmp = cardOrder.IndexOf(cards[i]).CompareTo(cardOrder.IndexOf(other.cards[i]));
                i++;
            }
            return cmp;
        }

        private const string cardOrder = "J23456789TQKA";

        private enum HandType
        {
            High,
            Pair,
            TwoPair,
            Three,
            Full,
            Four,
            Five,
        }
    }

    private const string input = @"Q94A5 121
AQ56Q 611
T6746 196
38853 434
69K88 178
44454 238
6KKKQ 913
4446J 425
9KK38 281
KQ8TK 319
A6A62 407
26J93 60
9A47Q 110
4464K 656
35373 558
Q222Q 258
T6377 243
TQ9JQ 554
J373A 525
59222 150
44JJ4 738
6Q662 452
JTK95 684
29KKK 697
63363 254
Q53A2 543
T55T5 479
5J39Q 743
T338T 629
8K5QT 561
K6T45 902
32544 857
AA8A8 421
66922 105
222J2 833
87878 949
A966A 661
4AA68 415
JQ2Q7 794
3QT93 618
665Q6 78
5599J 467
575A5 577
QJ699 846
KKKKQ 28
TKJTT 242
KJ868 115
59TTT 120
87J44 861
3A269 128
292T2 70
5AJ3K 412
J7TK8 916
JQJ6Q 241
22K22 590
T868J 584
A887A 971
77787 701
Q882J 758
4AA4A 880
3AJQ3 423
K3KK7 465
6956T 512
Q2Q52 530
98883 922
58AQ8 963
223TT 565
TT53T 626
6K244 843
6JQ3T 687
A7A7K 708
5TK62 457
4T44J 280
K3K22 470
Q8QK5 184
4Q9Q4 164
KKK66 934
9Q875 753
3Q98Q 230
22K2K 789
59949 553
33388 478
TTTAA 5
73787 501
4236J 222
4J665 749
6T666 431
J42K8 469
Q2722 598
33539 840
55445 204
98J88 302
87778 305
678J4 123
J464J 134
KKAKJ 32
98JQ9 329
Q9TQQ 36
A8569 380
T2447 726
3558T 609
K23JQ 335
AK9T3 303
98ATJ 156
TAJKT 291
3A4T7 894
48J28 327
8K3JT 386
TTT3K 486
Q8K28 528
9A4AA 444
3AA5Q 657
TT6TT 781
428JA 569
99899 176
63666 80
9AJ9A 273
8829A 364
68TQ4 151
A5849 202
47TT4 961
44483 999
3Q9A5 777
K383J 55
T2274 122
99J66 326
9939T 550
TQJ56 219
Q58T6 992
K93KK 937
44A38 797
T4428 266
6AAAJ 87
QQ444 505
4Q62Q 279
63623 983
969J3 223
8J888 357
JA3A3 354
88JQQ 878
A42J3 336
7A7A3 828
JQ68J 323
25555 883
Q488Q 669
T6784 395
T7524 683
98969 659
9894K 159
7Q2QQ 655
J7946 304
532QT 998
22QT2 935
9TT9T 564
K999J 769
5AK4A 968
8AAAA 490
92356 627
AA8J8 21
3Q343 986
TKTKT 824
K2465 770
J7774 535
8J97K 124
4444J 976
75Q79 283
JJJ44 668
4JK47 317
8A268 276
99Q6Q 389
J7J75 109
AAJKK 931
73366 347
37997 403
6572J 345
J48J4 763
A9AAQ 648
87A99 172
787A8 51
TJ24J 92
4T278 852
77T2Q 264
22J44 991
Q666Q 958
J74A9 318
TQJKA 265
JA5AK 454
A5J3J 808
3TT3J 796
62QJ5 539
5QA5Q 563
6QQQQ 495
T48J5 893
K9259 334
957AK 671
97393 481
K4848 216
KJJ3T 647
QQJJ7 125
354T3 921
55777 112
47TA7 810
J3J29 520
QJTQQ 874
TT272 277
QJQQ6 728
J55TK 518
22622 850
JQ236 155
Q7TTK 489
7TKTJ 41
AAAAJ 14
T3A3J 192
433K4 508
55545 398
77KQ9 361
A737J 967
A66J6 84
4745J 793
T2Q2T 666
5A54A 1000
753J2 714
55556 485
8TT8T 775
T9767 908
99299 907
227JJ 568
64952 62
555Q7 414
T86T9 943
JJ6K6 463
KKK3K 703
Q553J 370
97QQ7 596
44484 491
69367 730
J583K 206
35898 194
4K2QK 179
A56J7 405
4282Q 126
225JK 786
Q5J33 806
T44T8 941
99AAA 442
T88T8 670
3T963 69
Q33Q3 381
J2K2K 394
A99K8 167
739QK 400
8J93K 635
88688 891
5J232 66
57252 376
QQQQT 306
33232 628
AJJAA 915
72J22 750
A77AA 270
QQTTT 75
536A4 170
95795 61
KQJ2J 16
9TA6Q 617
999J8 33
QA63J 952
6QT28 246
T787J 127
3J373 496
J8QK4 691
383KK 477
97757 988
727J2 585
9992T 368
TT222 225
QQ33J 191
T656A 373
42222 873
44A4A 4
T9ATJ 725
QKJKK 166
TT633 153
833QJ 274
23993 22
TQ99Q 267
J9583 605
798TK 823
K33KK 331
A25J7 212
949Q9 514
62T66 911
T5239 851
97733 385
T2Q6J 845
AA66A 311
5Q5K3 129
89J98 152
Q6374 161
966K6 586
82J3A 175
QT7K7 308
QQ67Q 203
T9Q28 587
K2539 445
5554J 48
859J6 436
92A92 677
64JQ8 499
88885 9
88A3T 945
299J2 674
4QQ4K 435
64TJ8 630
238T7 169
TTTT4 784
AAAA2 290
QQKQQ 453
3924J 716
TT6KK 56
QQKTT 751
A2658 531
7AT3Q 145
JKKKK 948
QQQ8A 972
4484K 706
9A6Q7 64
343TT 46
AJQ2J 523
655T6 23
4KK4K 15
3TJK7 792
T78T4 527
3J57A 817
24444 675
K3459 599
AAAA9 734
663T6 658
5Q555 456
QQQ2Q 927
32594 960
T65KQ 841
28T76 44
97777 287
66544 429
J5K58 135
33Q33 236
222J3 804
64J59 532
4335A 707
3782A 195
Q29A6 358
89JA2 771
26QQ5 650
T99TJ 2
635J6 637
27723 106
6KQT8 404
T3TTT 868
3KJ27 560
6KAJ3 409
KTKKK 724
58T34 85
QA223 136
TTTKA 578
Q44A4 989
6J7J6 946
2Q2T6 890
34982 278
2242K 548
J9333 181
88448 686
757QQ 38
2KK2Q 340
74JJK 341
AJ8Q6 93
7JJ95 757
KK8K8 131
55252 83
9T557 269
523T4 722
9TA9K 207
K242K 209
26666 830
8888A 768
92QQQ 116
2222A 210
J4287 417
A55Q9 875
TA74A 330
Q87A7 160
2876J 694
28992 715
A5996 954
3J53J 90
2A2JK 259
8JA4A 936
8QTJK 638
TTQTT 113
JAT79 455
6J72A 606
26J42 94
T68KK 138
4QQA9 919
Q55Q3 869
22JJ5 570
4AJA3 837
3TQQA 250
AJAJ6 693
AK9AK 372
J4795 576
3235K 108
9A88A 799
2K5J6 933
95QA7 613
QJ7A4 838
262QQ 787
Q5J99 189
KK7T3 262
88779 342
6QQ6Q 174
A2656 119
J555J 393
3T2T6 723
562J3 767
Q46Q4 422
T3QQQ 897
56T28 99
95J32 182
6888T 574
TQTQQ 881
9T9T9 353
77KKK 289
QT555 625
56655 74
84442 72
JT883 81
QQ7Q7 297
5555K 337
8A2AJ 371
A4266 188
Q5J52 964
J6666 406
JQ382 825
66J4J 253
TT998 877
4382A 622
88AA5 460
29869 374
KK6J4 295
K792J 268
44474 612
66669 947
4Q9T3 990
66K56 511
JJ88Q 664
JTTTJ 923
49499 672
Q7683 855
JQQ66 232
J2KK7 383
995KA 65
4A272 52
223QQ 620
9J9J9 420
289AA 63
TTJTT 944
339K9 45
TA586 886
46767 836
65A66 871
22499 892
25533 292
JT4T4 676
447A2 592
Q2KKJ 733
777T7 26
57T77 39
73763 809
77774 978
T449Q 484
44996 382
7836A 464
99488 667
2333K 211
AJ8KT 524
K4444 284
6AAAA 483
5AQ65 940
T67J7 766
9QQQQ 811
JJAJA 816
Q8898 199
KKKK8 77
9J499 737
24472 352
KKK9K 709
7887K 631
588T2 688
K72T7 829
26578 831
72AQ3 348
38KQK 79
76676 545
KJ577 447
T2696 239
84A67 760
4T6QA 905
KKQT5 256
Q9K25 190
67TT4 820
QTAJ6 149
Q5JQ5 86
5QQ56 173
6KK9J 807
77789 288
AA666 815
7742A 788
A5T8Q 889
QTJ75 299
Q7QK7 717
4QQQ8 102
52J25 526
33633 953
T97A3 594
9A48K 17
J87QJ 76
2TTTT 433
2Q222 660
6626J 416
6T5TQ 387
3233A 778
257K8 597
J77J7 957
2777A 40
6K666 538
Q666K 271
K3J59 339
T4TJ9 441
TA3JT 193
65695 350
4JKK8 761
732KK 482
6AAK9 980
A8TA8 754
TKK4T 926
4K42Q 426
793KK 286
84Q95 25
87K8J 461
5588A 519
34Q82 472
86688 663
57QQ9 581
6K6K7 973
26T2J 870
2T983 100
K269J 720
752J7 712
QQQQJ 731
AJ5J5 31
59QQ5 437
K8TAQ 969
38737 408
58K88 756
54379 493
Q5Q44 1
AAKKK 614
99898 680
T7T77 552
3Q3QQ 111
JTQJT 604
824K7 739
J243T 965
49849 682
Q666J 513
Q27K5 970
888Q8 917
87364 762
885TA 180
5T44T 517
399JA 791
366T4 301
54858 928
2TT39 997
33A3T 579
82883 492
8A99A 765
93939 480
7K7K4 296
A5TQ5 785
3J22J 801
8J688 466
AAQ77 678
9A68J 856
TJ7TT 546
T8A96 529
33J85 649
J84JJ 369
J3QJQ 918
8T4AT 746
AT8TT 428
8JJJJ 685
T3A5T 430
JQJQQ 247
95926 818
58885 424
55K8K 312
JA6JK 231
7K8KK 154
T676T 993
5Q4A3 692
5T455 118
5QQ55 862
42422 607
Q4QA4 542
66AA7 57
A2K7K 547
454K4 711
5Q546 379
7365A 834
QA737 887
J3683 704
AJKT5 474
79299 772
8K2TA 476
6K6QJ 397
T36TT 349
68466 537
K34KK 427
92722 224
95444 309
44494 187
KQT2K 795
KK884 310
5T399 3
7A77A 803
5QQ5Q 975
4295K 89
363Q3 313
9KK9K 333
44577 632
J4456 747
J86A6 205
77J42 275
28442 43
3333J 654
3754J 744
49T85 411
7A7K7 904
6TTQK 853
474J7 168
44434 813
3676K 324
KJKJT 867
AJ77A 551
T42TT 681
AAQA3 589
KAT2T 197
A422Q 98
52JKK 619
Q6Q26 144
33666 600
T85T4 237
KTA88 536
Q68A7 752
J7676 282
7JK93 872
98TA7 399
35757 186
35533 634
KKAKQ 745
9JJ2J 163
T7K36 325
8TAJ5 251
28628 924
7K723 7
43626 139
2QAAA 826
77822 215
7KJ79 567
22J29 864
88223 201
645Q2 471
9959A 458
777J2 884
9KA5Q 462
6J464 88
Q639Q 10
5J595 974
J4454 146
33JA3 54
24228 49
J525T 996
2J22J 549
55JT5 20
AKJ8K 583
35635 645
295Q8 914
73TQK 615
23333 229
6J6K2 446
282J6 502
5J5A7 59
KQQK2 504
2KKKK 162
J3Q6J 510
A8AA5 249
ATA55 593
7A7A8 955
7333Q 137
6TA79 858
TTJT8 117
53939 509
8848A 198
9J2K3 402
5Q855 418
7KKK9 410
44Q44 498
82J83 540
K89QQ 696
82J87 293
95T99 995
54834 741
A7865 396
28262 73
T4JTT 557
T9T8T 977
K2K82 29
8686J 865
77766 101
Q5Q6Q 245
423A4 879
6AT29 515
83J88 432
AA62A 322
777A7 344
99994 727
4297A 451
3AQT8 157
2652J 355
4T4KA 640
KA976 158
22559 285
Q7K36 842
6655J 362
555J5 764
85887 367
35588 141
59995 805
56Q6Q 208
9T2Q2 494
T4Q58 616
A74Q8 366
8J885 779
96AJ9 702
83333 67
7TQ55 748
AJ6KQ 679
9999J 822
87Q6T 544
J77TJ 882
KK4KK 328
29995 651
3J33J 534
K3774 721
99599 556
KT528 27
38938 147
5QKA6 876
4K869 885
8TJQJ 300
6A22A 653
68A8J 263
6A666 588
439QQ 863
K8256 713
57466 213
923A7 384
A9696 255
7Q7JQ 755
95955 895
7Q279 800
43895 140
3T332 835
5KK55 298
KT2A4 591
3K33K 930
4TJ5T 500
KA2AK 690
45556 363
76TT7 903
333A3 13
J2TQQ 939
KAKKK 782
TT778 185
9Q99Q 143
77224 227
KJTKT 860
J4K73 956
737J3 377
3844J 440
34K4K 689
A5A66 966
2AQ5K 200
27774 896
227TQ 776
J448Q 673
45JT5 221
57J74 97
55355 47
8T888 641
AAA3A 50
392A2 774
KKK7K 475
QQ96J 35
K444J 96
44632 37
J52J4 220
56839 644
J5TTT 438
5Q49K 866
K8888 742
QK9K2 468
2J4TA 951
AA888 888
5K65Q 356
6T548 859
9J799 261
55KJK 165
56AQJ 320
77T82 844
T8AKK 316
QKKKQ 392
T9236 448
J7339 639
J6998 705
88338 82
7Q5A3 929
5JJ88 847
ATKQ7 783
JJ22T 332
T999Q 595
5A5AA 773
4484Q 987
7622Q 950
QQ44Q 602
8T43A 942
2A88A 95
J23T3 228
J7T86 42
3Q66T 981
QA28T 8
5522T 925
22437 260
TQ758 759
398JQ 582
32Q24 272
A3QQ8 315
9QK6Q 906
A25A4 580
49JK9 214
AQAAA 994
K565K 572
93792 812
QT8QQ 391
T9595 506
468Q9 827
A52J4 351
98779 718
344TT 848
552J5 832
A5JA2 573
KJK7K 473
63QQJ 699
99799 503
K2JKK 19
448A8 443
3K44Q 522
KJ8KK 662
K77JJ 623
A5Q8A 912
2K968 68
7TTJ8 307
Q4QQ7 621
T68QJ 962
KQ8Q8 375
45A55 360
27727 819
JQ53T 900
97979 798
7T6QA 642
77377 790
286TT 218
69A52 566
444A4 854
66TK9 610
J7777 740
34J3J 901
2QQQ2 821
5A6J3 58
863T6 910
777Q7 624
Q78K9 507
K3333 652
8J6Q9 439
29TQ5 91
Q2727 177
6JK6T 571
28J6T 240
884JT 450
58J85 541
4K4KA 24
KAQ23 314
22992 601
5J454 736
Q3332 107
AJ9Q5 710
TA979 521
6TJTQ 575
29242 735
3JJQ3 608
7J252 183
QJTQK 388
AAQAK 802
48775 248
65666 378
5389A 234
TTTQ2 979
7J7T7 71
Q894A 603
8Q86Q 6
55T55 343
33343 34
3636Q 103
5JJ5J 390
8996K 633
788A6 898
K7777 985
Q999K 114
35333 732
TK83A 53
88887 235
5TT5T 359
QQAAQ 132
QTTQ2 12
25975 346
2A72J 839
888JJ 719
KKKJT 365
4QJ44 533
TT335 636
5J2Q2 459
5556K 130
97A39 148
6A5Q8 133
4A45A 104
9TTTJ 233
8J8JJ 338
A9AJ3 413
QK2QT 497
Q929Q 401
44949 899
K666K 30
555AA 932
66Q6T 11
65258 559
A75AA 18
K7QK7 814
J7923 217
66667 226
55K5Q 909
9TJAA 562
632AJ 294
JJJJJ 488
55KQQ 849
J6336 780
2Q2J4 244
285J5 698
7QT3J 321
KKKK5 555
Q39K6 257
4A693 643
2T97A 665
26624 419
T9J95 646
KKJKJ 959
5J55K 700
6TA39 171
7TT74 487
9K494 729
44JJ9 449
TAK79 984
T2992 516
9QK6T 938
79272 982
22282 695
4JT95 920
56AK9 142
TT66T 252";
}