import 'dart:convert';

import 'package:best_flutter_ui_templates/apilib/apilib.dart';
import 'package:best_flutter_ui_templates/objectmodel/users.dart';
import 'package:best_flutter_ui_templates/piaggio/barcode_scan_screen.dart';
import 'package:best_flutter_ui_templates/piaggio/doctype_screen.dart';
import 'package:firebase_auth/firebase_auth.dart';
import 'package:flutter_session_manager/flutter_session_manager.dart';
import 'package:fluttertoast/fluttertoast.dart';
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
                                      hintText: "Email",
                                    )),
                                TextFormField(
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
                                    hintText: "Password",
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
                                  Navigator.push(
                                    context,
                                    MaterialPageRoute(
                                      builder: (context) => BarCodeScanScreen(),
                                    ),
                                  );
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
}
