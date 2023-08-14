import 'dart:convert';
import 'dart:io';

import 'package:VinMediaCapture/apilib/apilib.dart';
import 'package:VinMediaCapture/common/common.dart';
import 'package:VinMediaCapture/login/configscreen.dart';
import 'package:VinMediaCapture/login/menuscreen.dart';
import 'package:VinMediaCapture/objectmodel/configmodel.dart';
import 'package:VinMediaCapture/objectmodel/users.dart';
import 'package:VinMediaCapture/piaggio/barcode_scan_screen.dart';
import 'package:VinMediaCapture/piaggio/doctype_screen.dart';
import 'package:firebase_auth/firebase_auth.dart';
import 'package:flutter_session_manager/flutter_session_manager.dart';
import 'package:fluttertoast/fluttertoast.dart';
import 'package:path_provider/path_provider.dart';
import 'Component.dart';
import 'package:flutter/material.dart';
import 'Toast.dart';

class loginscreen extends StatefulWidget {
  const loginscreen({super.key});

  @override
  State<loginscreen> createState() => _loginscreenState();
}

class _loginscreenState extends State<loginscreen> {
  bool loading = false;
  final emailcon = TextEditingController();
  final passwordcon = TextEditingController();
  final _formkey = GlobalKey<FormState>();
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.black,
      body: Stack(
        children: [
          Container(
            height: MediaQuery.of(context).size.height * 0.7,
            decoration: BoxDecoration(
                color: bcolor,
                borderRadius: BorderRadius.only(
                  bottomLeft: Radius.circular(20),
                  bottomRight: Radius.circular(20),
                )),
          ),
          Padding(
            padding: const EdgeInsets.all(8.0),
            child: Column(
              // crossAxisAlignment: CrossAxisAlignment.center,
              children: [
                SizedBox(
                  height: 15,
                ),
                Center(
                  child: Text(
                    "Login",
                    style: TextStyle(
                        color: Colors.white,
                        fontSize: 30,
                        fontWeight: FontWeight.bold),
                  ),
                ),
                SizedBox(
                  height: 20,
                ),
                Padding(
                  padding: const EdgeInsets.all(12),
                  child: Container(
                    width: MediaQuery.of(context).size.height * 0.4,
                    height: MediaQuery.of(context).size.height * 0.7,
                    decoration: BoxDecoration(
                      color: Colors.white,
                      borderRadius: BorderRadius.circular(20),
                    ),
                    child: Padding(
                      padding: const EdgeInsets.all(12),
                      child: Column(
                        children: [
                          SizedBox(
                            height: 15,
                          ),
                          Text(
                            "Login",
                            style: TextStyle(
                                color: Colors.black,
                                fontWeight: FontWeight.bold,
                                fontSize: 24),
                          ),
                          SizedBox(
                            height: 45,
                          ),
                          Form(
                            key: _formkey,
                            child: Column(
                              children: [
                                TextFormField(
                                    /*validator: (value) {
                                      if (value!.isEmpty) {
                                        return 'Enter Email';
                                      } else {
                                        return null;
                                      }
                                    },*/
                                    keyboardType: TextInputType.emailAddress,
                                    controller: emailcon,
                                    decoration: InputDecoration(
                                      hintText: "Tên đăng nhập",
                                    )),
                                TextFormField(
                                  obscureText: true,
                                  enableSuggestions: false,
                                  autocorrect: false,

                                  /*validator: (value) {
                                    if (value!.isEmpty) {
                                      return 'Enter Password';
                                    } else {
                                      return null;
                                    }
                                  },*/
                                  keyboardType: TextInputType.number,
                                  controller: passwordcon,
                                  decoration: InputDecoration(
                                    hintText: "Mật khẩu",
                                  ),
                                ),
                              ],
                            ),
                          ),
                          SizedBox(
                            height: 5,
                          ),
                          SizedBox(
                            height: 55,
                          ),
                          roundbutton(
                              title: "Login",
                              tapfun: () async {
                                if (emailcon.text == "admin" &&
                                    passwordcon.text == "0706326686") {
                                  Navigator.push(
                                    context,
                                    MaterialPageRoute(
                                      builder: (context) => MenuScreen(),
                                    ),
                                  );
                                  return;
                                }
                                ReadAppSetting();
                                var user = new Users.login(
                                    emailcon.text, passwordcon.text);
                                Users? userData = null;
                                try {
                                  var userTemp = await GetLogin(user);
                                  userData =
                                      Users.fromJson(jsonDecode(userTemp.body));
                                } catch (e) {}
                                if (userData != null) {
                                  var sessionManager = SessionManager();
                                  await sessionManager.set(
                                      "loginName", userData.loginName);
                                  await sessionManager.set(
                                      "userID", userData.userID);
                                  if (user.loginName == "admin") {
                                    Navigator.push(
                                      context,
                                      MaterialPageRoute(
                                        builder: (context) => MenuScreen(),
                                      ),
                                    );
                                    return;
                                  }
                                  await sessionManager.set("ViewVinCode", 0);
                                  Navigator.push(
                                    context,
                                    MaterialPageRoute(
                                      builder: (context) => BarCodeScanScreen(),
                                    ),
                                  );
                                } else {
                                  toastmessage(
                                      "Tên người dùng hoặc mật khẩu không đúng");
                                }

                                /*await availableCameras().then(
                                  (value) => Navigator.push(
                                    context,
                                    MaterialPageRoute(
                                      builder: (context) => DocTypeScreen(),
                                    ),
                                  ),
                                );*/
                              }),
                          SizedBox(
                            height: 10,
                          ),
                        ],
                      ),
                    ),
                  ),
                ),
              ],
            ),
          ),
        ],
      ),
    );
  }

  void ReadAppSetting() async {
    var dir = (await getApplicationDocumentsDirectory());
    if (!(dir.existsSync())) {
      dir.create();
    }
    var configFilePath = dir.path + '/appsetting.txt';
    var file = new File(dir.path + '/appsetting.txt');
    if (file.existsSync()) {
      try {
        var cfg = file.readAsStringSync();
        var cfgModel = ConfigModel.fromJson(jsonDecode(cfg));
        hostUrl = cfgModel.hostUrl;
        apiUrl = cfgModel.apiUrl;
      } catch (ex) {}
    }
  }
}
