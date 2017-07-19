

var localApi = {
    getMerchantInfo: 'getMerchantInfo',
    printMerchat: 'printMerchat',
    getCommondityInfo: 'getCommondityInfo',
    printCommondity: 'printCommondity',
    getUserInfo: 'getUserInfo',
    getSkList:'getSkList',
}

var fieldApi = {
    userinfo: {
        ck_box:'true',
        s_id: '会员ID',
        u_name: '会员姓名',
        next_xf_time: '下次续费日期',
        next_fx_time:'下次返现日期',
        email_date: '发邮件日期',
        fm_date: '返码日期',
        email:'邮箱',
        s_create_time:"创建时间",
        s_bk_time:'绑卡时间',
    },
    posinfo: {
        ck_box:'true',
        s_id: '会员ID',
        s_name: '会员姓名',
        m_name: "刷卡商户",
        sk_date:"刷卡时间",
        sk_price:"刷卡金额",
    },
    xf_listInfo: {
        ck_box: 'true',
        s_id: '会员ID',
        s_name:'会员名称',
        m_name: '刷卡商户',
        c_name: '商品名称',
        commodity_price: '商品单价',
        commodity_num: '商品数量',
        sk_date:'刷卡时间',
    },
}