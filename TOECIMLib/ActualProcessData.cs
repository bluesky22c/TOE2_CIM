using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOECIMLib
{
    [Serializable]
    /// <summary>
    /// S6F73 보고에 들어가는 APD data.
    /// Console에서 모든 data를 정리해서, CIM에서 S6F73 보고할때 Console에게 data 요청 하고, 정리해서 보고한다.
    /// </summary>
    public class ActualProcessData
    {
        /// <summary>
        /// Glass 검사 시작 시간 hhmmss
        /// </summary>
        public string GLS_START_TIME { get; set; } = "      ";

        /// <summary>
        /// Glass 검사 종료 시간 hhmmss
        /// </summary>
        public string GLS_END_TIME { get; set; } = "      ";

        /// <summary>
        /// A cut LOT ID
        /// </summary>
        public string A_LOT_ID { get; set; } = "        ";

        /// <summary>
        /// B cut LOT ID
        /// </summary>
        public string B_LOT_ID { get; set; } = "        ";

        /// <summary>
        /// A cut glass id
        /// </summary>
        public string A_GLASS_ID { get; set; } = "        ";

        /// <summary>
        /// B cut glass id
        /// </summary>
        public string B_GLASS_ID { get; set; } = "        ";

        /// <summary>
        /// A cut 0도 비젼 얼라인 성공 여부 : 0 실패, 1: 성공, 2:Skip
        /// </summary>
        public string A_0_ALIGN { get; set; } = "2";

        /// <summary>
        /// B cut 0도 비젼 얼라인 성공 여부 : 0 실패, 1: 성공, 2:Skip
        /// </summary>
        public string B_0_ALIGN { get; set; } = "2";

        /// <summary>
        /// A cut 90도 비젼 얼라인 성공 여부 : 0 실패, 1: 성공, 2:Skip
        /// </summary>
        public string A_90_ALIGN { get; set; } = "2";

        /// <summary>
        /// B cut 90도 비젼 얼라인 성공 여부 : 0 실패, 1: 성공, 2:Skip
        /// </summary>
        public string B_90_ALIGN { get; set; } = "2";

        /// <summary>
        /// A cut 180도 비젼 얼라인 성공 여부 : 0 실패, 1: 성공, 2:Skip
        /// </summary>
        public string A_180_ALIGN { get; set; } = "2";

        /// <summary>
        /// B cut 180도 비젼 얼라인 성공 여부 : 0 실패, 1: 성공, 2:Skip
        /// </summary>
        public string B_180_ALIGN { get; set; } = "2";

        /// <summary>
        /// A cut 270도 비젼 얼라인 성공 여부 : 0 실패, 1: 성공, 2:Skip
        /// </summary>
        public string A_270_ALIGN { get; set; } = "2";

        /// <summary>
        /// B cut 270도 비젼 얼라인 성공 여부 : 0 실패, 1: 성공, 2:Skip
        /// </summary>
        public string B_270_ALIGN { get; set; } = "2";

        /// <summary>
        /// A cut 0도 비젼 얼라인 매칭률 : 0;Skip
        /// </summary>
        public string A_0_RATE { get; set; } = "0";

        /// <summary>
        /// B cut 0도 비젼 얼라인 매칭률 : 0;Skip
        /// </summary>
        public string B_0_RATE { get; set; } = "0";

        /// <summary>
        /// A cut 90도 비젼 얼라인 매칭률 : 0;Skip
        /// </summary>
        public string A_90_RATE { get; set; } = "0";

        /// <summary>
        /// B cut 90도 비젼 얼라인 매칭률 : 0;Skip
        /// </summary>
        public string B_90_RATE { get; set; } = "0";

        /// <summary>
        /// A cut 180도 비젼 얼라인 매칭률 : 0;Skip
        /// </summary>
        public string A_180_RATE { get; set; } = "0";

        /// <summary>
        /// B cut 180도 비젼 얼라인 매칭률 : 0;Skip
        /// </summary>
        public string B_180_RATE { get; set; } = "0";

        /// <summary>
        /// A cut 270도 비젼 얼라인 매칭률 : 0;Skip
        /// </summary>
        public string A_270_RATE { get; set; } = "0";

        /// <summary>
        /// B cut 270도 비젼 얼라인 매칭률 : 0;Skip
        /// </summary>
        public string B_270_RATE { get; set; } = "0";

        /// <summary>
        /// Scan 검사 속도
        /// </summary>
        public string SCAN_SPEED { get; set; } = "100";

        /// <summary>
        /// A cut Vision Defect 개수
        /// </summary>
        public string A_V_DEFECT { get; set; } = "0";

        /// <summary>
        /// B cut Vision Defect 개수
        /// </summary>
        public string B_V_DEFECT { get; set; } = "0";

        /// <summary>
        /// A cut 리뷰 이미지 개수
        /// </summary>
        public string A_REVIEW_CNT { get; set; } = "0";

        /// <summary>
        /// B cut 리뷰 이미지 개수
        /// </summary>
        public string B_REVIEW_CNT { get; set; } = "0";

        /// <summary>
        /// Active area scan 조명 level
        /// </summary>
        public string AA_SCAN_ILLU { get; set; } = "200";

        /// <summary>
        /// PAD scan 조명 level
        /// </summary>
        public string PAD_SCAN_ILLU { get; set; } = "125";

        /// <summary>
        /// Glass 검사시작 시간부터 Glass 검사 종료 시간까지의 시간
        /// </summary>
        public string TACT_TIME { get; set; } = "0";
    }
}
