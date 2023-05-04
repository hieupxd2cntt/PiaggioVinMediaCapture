import 'package:VinMediaCapture/apilib/apilib.dart';
import 'package:VinMediaCapture/common/common.dart';
import 'package:VinMediaCapture/login/Toast.dart';
import 'package:VinMediaCapture/model/doc_type_model_list_data.dart';
import 'package:VinMediaCapture/model/model_list_view.dart';
import 'package:VinMediaCapture/objectmodel/enum.dart';
import 'package:VinMediaCapture/objectmodel/mobileresult.dart';
import 'package:VinMediaCapture/piaggio/barcode_scan_screen.dart';
import 'package:VinMediaCapture/piaggio/vincode_scan_screen..dart';
import 'package:flutter/material.dart';
import 'package:flutter_session_manager/flutter_session_manager.dart';
import 'package:font_awesome_flutter/font_awesome_flutter.dart';
import '../hotel_booking/hotel_app_theme.dart';

class DetailModelScreen extends StatefulWidget {
  const DetailModelScreen({super.key});
  const DetailModelScreen.capturedImage(pictureFile, {super.key});
  @override
  _DetailModelScreenState createState() => _DetailModelScreenState();
}

int ViewVinCode = 1;
String currentSessionText = "";

class _DetailModelScreenState extends State<DetailModelScreen>
    with TickerProviderStateMixin {
  AnimationController? animationController;

  List<DocTypeModelListData> modelList = <DocTypeModelListData>[];
  final ScrollController _scrollController = ScrollController();

  @override
  void initState() {
    animationController = AnimationController(
        duration: const Duration(milliseconds: 1000), vsync: this);
    super.initState();
    var sessionManager = SessionManager();
    sessionManager
        .get("currentSession")
        .then((value) => currentSessionText = value);

    sessionManager.get("ViewVinCode").then((value) => ViewVinCode = value);

    DocTypeModelListData.getAttrDataType(currentSessionText).then((value) => {
          setState(() {
            modelList = value;
          })
        });
  }

  Future<bool> getData() async {
    await Future<dynamic>.delayed(const Duration(milliseconds: 200));
    return true;
  }

  @override
  void dispose() {
    animationController?.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Theme(
        data: HotelAppTheme.buildLightTheme(),
        child: Container(
          child: Scaffold(
            body: CreateStack(),
          ),
        ));
  }

  Stack CreateStack() {
    return Stack(
      children: <Widget>[
        InkWell(
          splashColor: Colors.transparent,
          focusColor: Colors.transparent,
          highlightColor: Colors.transparent,
          hoverColor: Colors.transparent,
          onTap: () {
            FocusScope.of(context).requestFocus(FocusNode());
          },
          child: Column(
            children: <Widget>[
              getAppBarUI(),
              Expanded(
                child: NestedScrollView(
                  controller: _scrollController,
                  headerSliverBuilder:
                      (BuildContext context, bool innerBoxIsScrolled) {
                    return <Widget>[
                      SliverPersistentHeader(
                        pinned: true,
                        floating: true,
                        delegate: ContestTabHeader(
                          getFilterBarUI(),
                        ),
                      ),
                    ];
                  },
                  body: Container(
                    color: HotelAppTheme.buildLightTheme().backgroundColor,
                    child: ListView.builder(
                      itemCount: modelList.length,
                      padding: const EdgeInsets.only(top: 8),
                      scrollDirection: Axis.vertical,
                      itemBuilder: (BuildContext context, int index) {
                        final int count =
                            modelList.length > 10 ? 10 : modelList.length;
                        final Animation<double> animation =
                            Tween<double>(begin: 0.0, end: 1.0).animate(
                                CurvedAnimation(
                                    parent: animationController!,
                                    curve: Interval((1 / count) * index, 1.0,
                                        curve: Curves.fastLinearToSlowEaseIn)));
                        animationController?.forward();
                        return ModelListView(
                          callback: () {
                            setState(() {});
                          },
                          modelData: modelList[index],
                          animation: animation,
                          animationController: animationController!,
                        );
                      },
                    ),
                  ),
                ),
              ),
              Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  children: <Widget>[
                    Container(
                        //width: MediaQuery.of(context).size.width * 0.3,
                        child: Expanded(
                      child: ElevatedButton(
                        onPressed: () async {
                          Navigator.push(
                            context,
                            MaterialPageRoute(
                                builder: (context) => VinCodeScanScreen()),
                          );
                        },
                        child: const Text('Back'),
                      ),
                    )),
                    Visibility(
                        visible: ViewVinCode == 1 ? false : true,
                        child: Container(

                            //width: MediaQuery.of(context).size.width * 0.3,
                            child: Expanded(
                                child: ElevatedButton(
                          onPressed: () async {
                            try {
                              bool hasError = false;
                              for (var element in modelList) {
                                if (element.isMandatory) {
                                  if (element.attrDataType ==
                                      EAttrDataType.VARCHAR) {
                                    if (element.textValue.isEmpty ||
                                        element.textValue.length == 0) {
                                      element.errorValidate =
                                          "Bạn phải nhập dữ liệu";
                                      hasError = true;
                                    } else {
                                      element.errorValidate = "";
                                    }
                                  } else if (element.attrDataType ==
                                      EAttrDataType.IMGCAPT) {
                                    if (element.assetImage.isEmpty &&
                                        element.textValue.length == 0) {
                                      element.errorValidate =
                                          "Bạn phải nhập dữ liệu ảnh";
                                      hasError = true;
                                    } else {
                                      element.textValue = element.assetImage;
                                      element.errorValidate = "";
                                    }
                                  }
                                }
                              }
                              if (hasError) {
                                setState(() {});
                                return;
                              }
                              var data = await PostDocTypeGuideItem(modelList);
                              var rs = MobileResult.fromJson(data.data);
                              if (rs.resultCode > 0) {
                                toastmessage('Lưu dữ liệu thành công');
                                //Gửi thành công. Xóa các file cũ. back về phiên mới
                                for (var element in modelList) {
                                  if (element.assetImage.length > 0) {
                                    await deleteFile(element.assetImage);
                                  }
                                }
                                Navigator.push(
                                  context,
                                  MaterialPageRoute(
                                      builder: (context) =>
                                          BarCodeScanScreen()),
                                );
                              }
                            } catch (e) {
                              var a = 1;
                            }
                          },
                          child: const Text('Lưu'),
                        )))),
                  ])
            ],
          ),
        ),
      ],
    );
  }

  Future<Widget> getListUI() async {
    return Container(
      decoration: BoxDecoration(
        color: HotelAppTheme.buildLightTheme().backgroundColor,
        boxShadow: <BoxShadow>[
          BoxShadow(
              color: Colors.grey.withOpacity(0.2),
              offset: const Offset(0, -2),
              blurRadius: 8.0),
        ],
      ),
      child: Column(
        children: <Widget>[
          Container(
            height: MediaQuery.of(context).size.height - 156 - 50,
            child: FutureBuilder<bool>(
              future: getData(),
              builder: (BuildContext context, AsyncSnapshot<bool> snapshot) {
                if (!snapshot.hasData) {
                  return const SizedBox();
                } else {
                  return ListView.builder(
                    itemCount: modelList.length,
                    scrollDirection: Axis.vertical,
                    itemBuilder: (BuildContext context, int index) {
                      final int count =
                          modelList.length > 10 ? 10 : modelList.length;
                      final Animation<double> animation =
                          Tween<double>(begin: 0.0, end: 1.0).animate(
                              CurvedAnimation(
                                  parent: animationController!,
                                  curve: Interval((1 / count) * index, 1.0,
                                      curve: Curves.fastOutSlowIn)));
                      animationController?.forward();

                      return ModelListView(
                        callback: () {},
                        modelData: modelList[index],
                        animation: animation,
                        animationController: animationController!,
                      );
                    },
                  );
                }
              },
            ),
          )
        ],
      ),
    );
  }

  Widget getHotelViewList() {
    final List<Widget> hotelListViews = <Widget>[];
    for (int i = 0; i < modelList.length; i++) {
      final int count = modelList.length;
      final Animation<double> animation =
          Tween<double>(begin: 0.0, end: 1.0).animate(
        CurvedAnimation(
          parent: animationController!,
          curve: Interval((1 / count) * i, 1.0, curve: Curves.fastOutSlowIn),
        ),
      );
      hotelListViews.add(
        ModelListView(
          callback: () {},
          modelData: modelList[i],
          animation: animation,
          animationController: animationController!,
        ),
      );
    }
    animationController?.forward();
    return Column(
      children: hotelListViews,
    );
  }

  Widget getSearchBarUI() {
    return Padding(
      padding: const EdgeInsets.only(left: 16, right: 16, top: 8, bottom: 8),
      child: Row(
        children: <Widget>[
          Expanded(
            child: Padding(
              padding: const EdgeInsets.only(right: 16, top: 8, bottom: 8),
              child: Container(
                decoration: BoxDecoration(
                  color: HotelAppTheme.buildLightTheme().backgroundColor,
                  borderRadius: const BorderRadius.all(
                    Radius.circular(38.0),
                  ),
                  boxShadow: <BoxShadow>[
                    BoxShadow(
                        color: Colors.grey.withOpacity(0.2),
                        offset: const Offset(0, 2),
                        blurRadius: 8.0),
                  ],
                ),
                child: Padding(
                  padding: const EdgeInsets.only(
                      left: 16, right: 16, top: 4, bottom: 4),
                  child: TextField(
                    onChanged: (String txt) {},
                    style: const TextStyle(
                      fontSize: 18,
                    ),
                    cursorColor: HotelAppTheme.buildLightTheme().primaryColor,
                    decoration: InputDecoration(
                      border: InputBorder.none,
                      hintText: 'London...',
                    ),
                  ),
                ),
              ),
            ),
          ),
          Container(
            decoration: BoxDecoration(
              color: HotelAppTheme.buildLightTheme().primaryColor,
              borderRadius: const BorderRadius.all(
                Radius.circular(38.0),
              ),
              boxShadow: <BoxShadow>[
                BoxShadow(
                    color: Colors.grey.withOpacity(0.4),
                    offset: const Offset(0, 2),
                    blurRadius: 8.0),
              ],
            ),
            child: Material(
              color: Colors.transparent,
              child: InkWell(
                borderRadius: const BorderRadius.all(
                  Radius.circular(32.0),
                ),
                onTap: () {
                  //FocusScope.of(context).requestFocus(FocusNode());
                },
                child: Padding(
                  padding: const EdgeInsets.all(16.0),
                  child: Icon(FontAwesomeIcons.magnifyingGlass,
                      size: 20,
                      color: HotelAppTheme.buildLightTheme().backgroundColor),
                ),
              ),
            ),
          ),
        ],
      ),
    );
  }

  Widget getFilterBarUI() {
    return Stack(
      children: <Widget>[
        Positioned(
          top: 0,
          left: 0,
          right: 0,
          child: Container(
            height: 24,
            decoration: BoxDecoration(
              color: HotelAppTheme.buildLightTheme().backgroundColor,
            ),
          ),
        ),
        const Positioned(
          top: 0,
          left: 0,
          right: 0,
          child: Divider(
            height: 1,
          ),
        )
      ],
    );
  }

  Widget getAppBarUI() {
    return Container(
      decoration: BoxDecoration(
        color: HotelAppTheme.buildLightTheme().backgroundColor,
        boxShadow: <BoxShadow>[
          BoxShadow(
              color: Colors.grey.withOpacity(0.2),
              offset: const Offset(0, 2),
              blurRadius: 8.0),
        ],
      ),
      child: Padding(
        padding: EdgeInsets.only(
            top: MediaQuery.of(context).padding.top, left: 8, right: 8),
        child: Row(
          children: <Widget>[
            Container(
              alignment: Alignment.centerLeft,
              width: AppBar().preferredSize.height + 40,
              height: AppBar().preferredSize.height,
              child: Material(
                color: Colors.transparent,
                child: InkWell(
                  borderRadius: const BorderRadius.all(
                    Radius.circular(32.0),
                  ),
                  onTap: () {
                    Navigator.pop(context);
                  },
                  child: Padding(
                    padding: const EdgeInsets.all(8.0),
                    child: Icon(Icons.arrow_back),
                  ),
                ),
              ),
            ),
            Expanded(
              child: Center(
                child: Text(
                  currentSessionText,
                  style: TextStyle(
                    fontWeight: FontWeight.w600,
                    fontSize: 22,
                  ),
                ),
              ),
            ),
            Container(
              width: AppBar().preferredSize.height + 40,
              height: AppBar().preferredSize.height,
            )
          ],
        ),
      ),
    );
  }
}

class ContestTabHeader extends SliverPersistentHeaderDelegate {
  ContestTabHeader(
    this.searchUI,
  );
  final Widget searchUI;

  @override
  Widget build(
      BuildContext context, double shrinkOffset, bool overlapsContent) {
    return searchUI;
  }

  @override
  double get maxExtent => 52.0;

  @override
  double get minExtent => 52.0;

  @override
  bool shouldRebuild(SliverPersistentHeaderDelegate oldDelegate) {
    return false;
  }
}
